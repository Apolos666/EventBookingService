var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Application services
builder.Services.AddApplicationServices(assembly);
builder.Services.AddBasketServices();
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddGrpcServices(builder.Configuration);
builder.Services.AddDataServices(builder.Configuration);

// Authentication and Authorization services
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddCustomAuthorization();

// Async Communication Services
builder.Services.AddMessageBrokerServices(builder.Configuration, Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapCarter();
app.MapGrpcService<BasketService>();
app.Run();
