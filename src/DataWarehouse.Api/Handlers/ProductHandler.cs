using DataWarehouse.Application.Services;

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
}