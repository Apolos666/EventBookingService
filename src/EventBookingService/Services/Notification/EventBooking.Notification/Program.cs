var builder = WebApplication.CreateBuilder(args);

// 
builder.Services.AddCarter();
builder.Services.AddSignalR();

// Security services
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors(options =>
    {
        options.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(origin => true);
    });    
}

// Register the hubs
app.MapHub<NotificationHub>("/notifications");

app.MapCarter();

app.Run();
