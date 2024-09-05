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

// Validators
builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

app.MapCarter();
app.Run();