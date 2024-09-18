var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
    .AddJwtBearer("webapp", options =>
    {
        options.Authority = builder.Configuration["Keycloak:Authority"];
        options.MetadataAddress = builder.Configuration["Keycloak:MetadataAddress"];
        options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("Keycloak:RequireHttpsMetadata");
        options.Audience = builder.Configuration["Keycloak:Audience:webapp"]; 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = false,
            ValidIssuers = builder.Configuration.GetValue<IEnumerable<string>>("Keycloak:ValidIssuers"),
            ValidateAudience = true,
            ValidAudiences = builder.Configuration.GetValue<IEnumerable<string>>("Keycloak:ValidAudiences:webapp")
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AuthenticatedUser", builder =>
    {
        builder.RequireAuthenticatedUser();
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));   

var app = builder.Build();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();
