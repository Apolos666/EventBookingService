var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Application services
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserIdentityAccessor, HttpUserIdentityAccessor>();

// Data services
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
    config.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate; 
}).UseLightweightSessions();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.Decorate<IEventRepository, CachedEventRepository>();

builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = builder.Configuration.GetConnectionString("Redis")!;
});

// Validators
builder.Services.AddValidatorsFromAssembly(assembly);

// Authentication and Authorization services
builder.Services.AddAuthentication("webapp")
    .AddJwtBearer("webapp", options =>
    {
        options.Authority = builder.Configuration["Keycloak:Authority"];
        options.MetadataAddress = builder.Configuration["Keycloak:MetadataAddress"];
        options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("Keycloak:RequireHttpsMetadata");
        options.Audience = builder.Configuration["Keycloak:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = false,
            ValidIssuers = builder.Configuration.GetValue<IEnumerable<string>>("Keycloak:ValidIssuers"),
            ValidateAudience = true,
            ValidAudiences = builder.Configuration.GetValue<IEnumerable<string>>("Keycloak:ValidAudiences")
        };
    });

builder.Services
    .AddAuthorization()
    .AddKeycloakAuthorization()
    .AddAuthorizationBuilder()
    .AddCustomAuthorizationPolicies();

// HttpClients
builder.Services.AddClientCredentialsTokenManagement()
    .AddClient("storage.client", client =>
    {
        client.TokenEndpoint = builder.Configuration["Keycloak:TokenEndpoint"];
        client.ClientId = builder.Configuration["Keycloak:ClientIdStorage"];
        client.ClientSecret = builder.Configuration["Keycloak:ClientSecretStorage"];
    });

// Grpc Clients
builder.Services.AddGrpcClient<ImageStorage.ImageStorageClient>(options =>
    {
        options.Address = new Uri(builder.Configuration["GrpcSettings:StorageUrl"]);
    })
    .AddCallCredentials(async (context, metadata, serviceProvider) =>
    {
        var provider = serviceProvider.GetRequiredService<IClientCredentialsTokenManagementService>();
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        
        var response = provider.GetAccessTokenAsync("storage.client");
    
        metadata.Add("Authorization", $"Bearer {response.Result.AccessToken}");
    })
    .ConfigurePrimaryHttpMessageHandler(_ =>
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        
        return handler;
    });

// Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

// Background services
builder.Services.AddHostedService<EventStartDateChecker>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();

app.Run();
