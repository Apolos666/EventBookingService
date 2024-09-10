var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNotificationServices(builder.Configuration);

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

app.UseAuthentication();
app.UseAuthorization();

// Register the hubs
app.MapCustomHubs();

app.MapCarter();

app.Run();
