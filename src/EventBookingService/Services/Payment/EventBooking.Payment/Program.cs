var builder = WebApplication.CreateBuilder(args);

// Application servicess
builder.Services.AddCarter();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserIdentityAccessor, HttpUserIdentityAccessor>();
builder.Services.AddScoped<IPaymentService<StripeCheckoutRequest, StripeCheckoutResponse>>(serviceProvider =>
{
    var publisher = serviceProvider.GetRequiredService<IPublishEndpoint>();
    return new StripePaymentService(
        builder.Configuration["Stripe:Secret"],
        publisher);
});

// HttpClients
builder.Services.AddClientCredentialsTokenManagement()
    .AddClient("basket.client", client =>
    {
        client.TokenEndpoint = builder.Configuration["Keycloak:TokenEndpoint"];
        client.ClientId = builder.Configuration["Keycloak:ClientId"];
        client.ClientSecret = builder.Configuration["Keycloak:ClientSecret"];
    });

// GRPC Clients
builder.Services.AddGrpcClient<BasketProtoService.BasketProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:BasketUrl"]);;
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
        options.Authority = builder.Configuration["Keycloak:Authority"];
        options.MetadataAddress = builder.Configuration["Keycloak:MetadataAddress"];
        options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("Keycloak:RequireHttpsMetadata");
        options.Audience = builder.Configuration["Keycloak:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = true,
            ValidIssuers = builder.Configuration.GetValue<IEnumerable<string>>("Keycloak:ValidIssuers"),
            ValidateAudience = true,
            ValidAudiences = builder.Configuration.GetValue<IEnumerable<string>>("Keycloak:ValidAudiences")
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

// Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

var app = builder.Build();
    
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();
app.Run();
