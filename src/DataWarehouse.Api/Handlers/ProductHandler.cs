using DataWarehouse.Application.DTOs;
using DataWarehouse.Application.Services;
using DataWarehouse.Domain;

namespace DataWarehouse.Api.Handlers;


public static class ProductHandler
{
    public static async Task<IResult> GetProducts(ProductService service)
    {
        var products = await service.GetAllProductsAsync();
        return Results.Ok(ApiResponse<IEnumerable<Product>>.Ok(products, "Get Products Successful"));
    }

    public static async Task<IResult> GetProductById(int id, ProductService service)
    {
        var product = await service.GetProductByIdAsync(id);
        if(product is null)
            return Results.NotFound(ApiResponse<object>.Fail("Product not found"));

        return Results.Ok(ApiResponse<object>.Ok(product, "Get Product Successful"));   
    }

    public static async Task<IResult> AddProduct(ProductRequest request, ProductService service)
    {
        var body = new Product(){
            Sku = request.Sku,
            ProductName = request.ProductName,
            Category = request.Category
        };
        
        var product = await service.AddProductAsync(body);

        return Results.Ok(ApiResponse<Product>.Ok(product, "Add Product Successful"));
    }
}