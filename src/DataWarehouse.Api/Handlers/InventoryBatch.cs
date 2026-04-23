using DataWarehouse.Application.DTOs;
using DataWarehouse.Application.Services;
using DataWarehouse.Domain;

namespace DataWarehouse.Api.Handlers;

public static class InventoryBatchHandler
{
    public static async Task<IResult> GetAll(InventoryBatchService service)
    {
        var batches = await service.GetAllAsync();
        return Results.Ok(ApiResponse<IEnumerable<InventoryBatch>>.Ok(batches, "Get Inventory Batches Successful"));
    }

    public static async Task<IResult> GetByBatchId (Guid batchId, InventoryBatchService service)
    {
        var batch = await service.GetByIdAsync(batchId);
        if(batch is null)
            return Results.NotFound(ApiResponse<object>.Fail("Inventory Batch not found"));

        return Results.Ok(ApiResponse<object>.Ok(batch, "Get Inventory Batch Successful"));
    }

    public static async Task<IResult> Add(InventoryBatchRequest request, InventoryBatchService service)
    {
        if(!Enum.TryParse<BatchStatus>(request.Status, ignoreCase: true, out var status))
            return Results.BadRequest(ApiResponse<object>.Fail("Invalid status. Use: Available, Empty, or Expired."));

        var batch = new InventoryBatch
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            RemainingQty = request.Quantity,
            EntryDate = request.EntryDate,
            PicInId = request.PicInId,
            Status = status,
        };

        var result = await service.AddAsync(batch);
        return Results.Ok(ApiResponse<InventoryBatch>.Ok(result, "Add Inventory Batch Successful"));
    }
}