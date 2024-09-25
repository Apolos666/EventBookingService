var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Application services
builder.Services.AddApplicationServices(assembly);
builder.Services.AddRedisCache(builder.Configuration);

// Data services
builder.Services.AddDataServices(builder.Configuration);

// Authentication and Authorization services
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddCustomAuthorization();

// HttpClients
builder.Services.AddCustomHttpClients(builder.Configuration);

// Grpc Clients
builder.Services.AddGrpcClients(builder.Configuration);

// Async Communication Services
builder.Services.AddMessageBrokerServices(builder.Configuration, assembly);

// Background services
builder.Services.AddBackgroundServices();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();

app.Run();