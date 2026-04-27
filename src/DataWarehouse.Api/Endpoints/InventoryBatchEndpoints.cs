using DataWarehouse.Api.Handlers;
using DataWarehouse.Application.DTOs;
using DataWarehouse.Domain;

namespace DataWarehouse.Api.Endpoints;

public static class InventoryBatchEndpoints
{
    public static void MapInventoryBatchEndpoints(this WebApplication app)
    {
        app.MapGet("/inventory-batches", InventoryBatchHandler.GetAll)
            .WithName("GetAllIventoryBatches")
            .WithTags("Inventory Batches")
            .RequireAuthorization()
            .Produces<PagedApiResponse<IEnumerable<InventoryBatch>>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapGet("/inventory-batches/{batchId:guid}", InventoryBatchHandler.GetByBatchId)
            .WithName("GetInventoryBatchById")
            .WithTags("Inventory Batches")
            .RequireAuthorization()
            .Produces<ApiResponse<InventoryBatch>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapPost("/inventory-batches", InventoryBatchHandler.Add)
            .WithName("AddInventoryBatch")
            .WithTags("Inventory Batches")
            .RequireAuthorization()
            .Produces<ApiResponse<InventoryBatch>>(StatusCodes.Status200OK)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}