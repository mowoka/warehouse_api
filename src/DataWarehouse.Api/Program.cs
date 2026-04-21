using DataWarehouse.Application.Interfaces;
using DataWarehouse.Application.Services;
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

// Seed dummy data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataWarehouseContext>();
    context.Products.AddRange(
        new Product { Id = 1, Sku = "SKU-001", ProductName = "Laptop Pro", Category = "Electronics" },
        new Product { Id = 2, Sku = "SKU-002", ProductName = "Mechanical Keyboard", Category = "Electronics" },
        new Product { Id = 3, Sku = "SKU-003", ProductName = "Office Chair", Category = "Furniture" }
    );
    context.SaveChanges();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGet("/products", async (ProductService service) =>
{
    var products = await service.GetAllProductsAsync();
    return Results.Ok(products);
})
.WithName("GetAllProducts")
.WithTags("Products");

app.MapGet("/products/{id}", async (int id, ProductService service) =>
{
    var product = await service.GetProductByIdAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
})
.WithName("GetProductById")
.WithTags("Products");

app.Run();
