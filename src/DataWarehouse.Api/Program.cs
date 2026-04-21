using DataWarehouse.Application.Interfaces;
using DataWarehouse.Application.Services;
using DataWarehouse.Api.Endpoints;
using DataWarehouse.Infrastructure;
using DataWarehouse.Infrastructure.Repositories;
using DataWarehouse.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataWarehouseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

// Register seeders
builder.Services.AddScoped<ISeeder, ProductSeeder>();
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
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// Register endpoints
app.MapProductEndpoints();

app.Run();
