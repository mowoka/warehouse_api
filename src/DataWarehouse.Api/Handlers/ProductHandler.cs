using DataWarehouse.Application.DTOs;
using DataWarehouse.Application.Services;
using DataWarehouse.Domain;

namespace DataWarehouse.Api.Handlers;


public static class ProductHandler
{
    public static async Task<IResult> GetProducts(ProductService service)
    {
        var products = await service.GetAllProductsAsync();
        return Results.Ok(products);
    }

    public static async Task<IResult> GetProductById(int id, ProductService service)
    {
        var product = await service.GetProductByIdAsync(id);
        return product is not null ? Results.Ok(product) : Results.NotFound();
    }

    public static async Task<IResult> AddProduct(ProductRequest request, ProductService service)
    {
        var body = new Product(){
            Sku = request.Sku,
            ProductName = request.ProductName,
            Category = request.Category
        };
        
        var product = await service.AddProductAsync(body);

        return Results.Ok(product);
    }
}