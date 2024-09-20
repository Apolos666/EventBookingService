var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

// Authentication and Authorization services
builder.Services.AddAuthentication("storage_service")
    .AddJwtBearer("storage_service", options =>
    {
        options.Authority = builder.Configuration["Keycloak:Authority"];
        options.MetadataAddress = builder.Configuration["Keycloak:MetadataAddress"];
        options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("Keycloak:RequireHttpsMetadata");
        options.Audience = builder.Configuration["Keycloak:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = true,
            ValidIssuers = builder.Configuration.GetValue<IEnumerable<string>>("Keycloak:ValidIssuers"),
            ValidateAudience = true,
            ValidAudiences = builder.Configuration.GetValue<IEnumerable<string>>("Keycloak:ValidAudiences")    
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
   FileProvider = new PhysicalFileProvider(
      Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")),
    RequestPath = "/StaticFiles"
});

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<ImageStorageService>();

app.Run();
