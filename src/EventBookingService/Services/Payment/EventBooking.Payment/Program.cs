var builder = WebApplication.CreateBuilder(args);

// Application servicess
builder.Services.AddCarter();

builder.Services.AddDistributedMemoryCache();

// HttpClients
builder.Services.AddClientCredentialsTokenManagement()
    .AddClient("basket.client", client =>
    {
        client.TokenEndpoint = "http://localhost:8090/realms/Event-Booking-Service/protocol/openid-connect/token";
        client.ClientId = "event-booking-service-basket";
        client.ClientSecret = "T74fx6zXr4cOCmujRQ9Pdk3eR595LIJA";
    });

// Authentication and Authorization services
builder.Services.AddAuthentication("payment_service")
    .AddJwtBearer("payment_service", options =>
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

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();
app.Run();
