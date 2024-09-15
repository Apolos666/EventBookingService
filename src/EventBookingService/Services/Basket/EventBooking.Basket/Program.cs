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

// Data Services
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
    config.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
    config.Schema.For<EventCart>().Identity(ec => ec.UserId);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = builder.Configuration.GetConnectionString("Redis")!;
});

// GRPC Services
builder.Services.AddGrpc();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
}).ConfigurePrimaryHttpMessageHandler(_ =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    
    return handler;
});

// Validators
builder.Services.AddValidatorsFromAssembly(assembly);

// Authentication and Authorization services

builder.Services.AddAuthentication("webapp")
    .AddJwtBearer("webapp", options =>
    {
        options.Authority = "http://localhost:8090/realms/Event-Booking-Service";
        options.MetadataAddress = "http://localhost:8090/realms/Event-Booking-Service/.well-known/openid-configuration";
        options.RequireHttpsMetadata = false;
        options.Audience = "basket_service"; 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = true,
            ValidIssuers = ["http://localhost:8090/auth/realms/Event-Booking-Service"],
            ValidateAudience = true,
            ValidAudiences = ["basket_service"]   
        };
    })
    .AddJwtBearer("basket_service", options =>
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

builder.Services
    .AddAuthorization()
    .AddKeycloakAuthorization()
    .AddAuthorizationBuilder()  
    .AddCustomAuthorizationPolicies();

// Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapCarter();
app.MapGrpcService<BasketService>();
app.Run();
