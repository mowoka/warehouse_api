using DataWarehouse.Api.Handlers;

namespace DataWarehouse.Api.Endpoints;

public static class InventoryBatchEndpoints
{
    public static void MapInventoryBatchEndpoints(this WebApplication app)
    {
        app.MapGet("/inventory-batches", InventoryBatchHandler.GetAll)
            .WithName("GetAllIventoryBatches")
            .WithTags("Inventory Batches")
            .RequireAuthorization();

        app.MapGet("/inventory-batches/{batchId:guid}", InventoryBatchHandler.GetByBatchId)
            .WithName("GetInventoryBatchById")
            .WithTags("Inventory Batches")
            .RequireAuthorization();

        app.MapPost("/inventory-batches", InventoryBatchHandler.Add)
            .WithName("AddInventoryBatch")
            .WithTags("Inventory Batches")
            .RequireAuthorization();
    }
}