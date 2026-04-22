using DataWarehouse.Application.Interfaces;
using DataWarehouse.Application.Services;
using DataWarehouse.Api.Endpoints;
using DataWarehouse.Infrastructure;
using DataWarehouse.Infrastructure.Repositories;
using DataWarehouse.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using DataWarehouse.Api.Extensions;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Configure OpenAPI with JWT support
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, ct) =>
    {
        document.Components ??= new();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Enter your JWT token (without 'Bearer ' prefix)"
        });
        return Task.CompletedTask;
    });
});

// register infrastructure services
builder.Services.AddDbContext<DataWarehouseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddJwtAuthentication(builder.Configuration);

// register repositories and services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IInventoryBatchRepository, InventoryBatchRepository>();
builder.Services.AddScoped<InventoryBatchService>();


// Register seeders
builder.Services.AddScoped<ISeeder, ProductSeeder>();
builder.Services.AddScoped<ISeeder, UserSeeder>();
builder.Services.AddScoped<DatabaseSeeder>();

var app = builder.Build();

// Run seeders
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Authentication = new ScalarAuthenticationOptions
        {
            PreferredSecuritySchemes = ["Bearer"]
        };
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Register endpoints
app.MapProductEndpoints();
app.MapUserEndpoints();
app.MapInventoryBatchEndpoints();

app.Run();
