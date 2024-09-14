var builder = WebApplication.CreateBuilder(args);

// Application servicess
builder.Services.AddCarter();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserIdentityAccessor, HttpUserIdentityAccessor>();

// HttpClients
builder.Services.AddClientCredentialsTokenManagement()
    .AddClient("basket.client", client =>
    {
        client.TokenEndpoint = "http://localhost:8090/realms/Event-Booking-Service/protocol/openid-connect/token";
        client.ClientId = "event-booking-service-basket";
        client.ClientSecret = "T74fx6zXr4cOCmujRQ9Pdk3eR595LIJA";
    });

// GRPC Clients
builder.Services.AddGrpcClient<BasketProtoService.BasketProtoServiceClient>(options =>
{
    options.Address = new Uri("https://localhost:5052");
}).AddCallCredentials(async (context, metadata, serviceProvider) =>
{
    var provider = serviceProvider.GetRequiredService<IClientCredentialsTokenManagementService>();
    var response = provider.GetAccessTokenAsync("basket.client");
    metadata.Add("Authorization", $"Bearer {response.Result.AccessToken}");
}).ConfigurePrimaryHttpMessageHandler(_ =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    
    return handler;
});

// Authentication and Authorization services
builder.Services.AddAuthentication("web_app")
    .AddJwtBearer("web_app", options =>
    {
        options.Authority = "http://localhost:8090/realms/Event-Booking-Service";
        options.MetadataAddress = "http://localhost:8090/realms/Event-Booking-Service/.well-known/openid-configuration";
        options.RequireHttpsMetadata = false;
        options.Audience = "payment_service";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = true,
            ValidIssuers = ["http://localhost:8090/auth/realms/Event-Booking-Service"],
            ValidateAudience = true,
            ValidAudiences = ["payment_service"]
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
});

var app = builder.Build();
    
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();
app.Run();
