using DataWarehouse.Application.Services;

namespace DataWarehouse.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
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
    }
}
