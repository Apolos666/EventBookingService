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
builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);

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

app.Run();
