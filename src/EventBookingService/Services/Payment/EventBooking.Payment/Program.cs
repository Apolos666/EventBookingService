var builder = WebApplication.CreateBuilder(args);

// Application services
builder.Services.AddApplicationServices(builder.Configuration);

// HttpClients
builder.Services.AddCustomHttpClients(builder.Configuration);

// GRPC Clients
builder.Services.AddGrpcClients(builder.Configuration);

// Authentication and Authorization services
builder.Services.AddCustomAuthentication(builder.Configuration);

// CORS
builder.Services.AddCustomCors();

// Async Communication Services
builder.Services.AddMessageBrokerServices(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();
app.Run();