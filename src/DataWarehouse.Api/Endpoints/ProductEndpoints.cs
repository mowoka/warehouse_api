using DataWarehouse.Api.Handlers;

namespace DataWarehouse.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/products", ProductHandler.GetProducts)
        .WithName("GetAllProducts")
        .WithTags("Products");

        app.MapGet("/products/{id}", ProductHandler.GetProductById)
        .WithName("GetProductById")
        .WithTags("Products");
    }
}

