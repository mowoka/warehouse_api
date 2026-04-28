using DataWarehouse.Api.Handlers;
using DataWarehouse.Application.DTOs;
using DataWarehouse.Domain;

namespace DataWarehouse.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/products", ProductHandler.GetProducts)
            .WithName("GetAllProducts")
            .WithTags("Products")
            .RequireAuthorization()
            .Produces<ApiResponse<IEnumerable<Product>>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapGet("/products/{id}", ProductHandler.GetProductById)
            .WithName("GetProductById")
            .WithTags("Products")
            .RequireAuthorization()
            .Produces<ApiResponse<Product>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapPost("/products", ProductHandler.AddProduct)
            .WithName("AddProduct")
            .WithTags("Products")
            .RequireAuthorization()
            .Produces<ApiResponse<Product>>(StatusCodes.Status201Created)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapPatch("/products/{id}", ProductHandler.UpdateProduct)
            .WithName("UpdateProduct")
            .WithTags("Products")
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapGet("/product-search", ProductHandler.SearchProducts)
            .WithName("SearchProducts")
            .WithTags("Products")
            .RequireAuthorization()
            .Produces<ApiResponse<IEnumerable<Product>>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}

