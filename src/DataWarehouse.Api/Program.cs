using DataWarehouse.Application.Interfaces;
using DataWarehouse.Application.Services;
using DataWarehouse.Api.Endpoints;
using DataWarehouse.Domain;
using DataWarehouse.Infrastructure;
using DataWarehouse.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataWarehouseContext>(options =>
    options.UseInMemoryDatabase("DataWarehouseDb"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// Register endpoints
app.MapProductEndpoints();

app.Run();
