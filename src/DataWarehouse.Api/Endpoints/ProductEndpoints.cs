using DataWarehouse.Api.Handlers;

namespace DataWarehouse.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/products", ProductHandler.GetProducts)
        .WithName("GetAllProducts")
        .WithTags("Products")
        .RequireAuthorization();

        app.MapGet("/products/{id}", ProductHandler.GetProductById)
        .WithName("GetProductById")
        .WithTags("Products")
        .RequireAuthorization();

        app.MapPost("/products", ProductHandler.AddProduct)
        .WithName("AddProduct")
        .WithTags("Products")
        .RequireAuthorization();
    }
}

