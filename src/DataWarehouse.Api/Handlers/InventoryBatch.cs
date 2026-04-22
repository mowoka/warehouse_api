using DataWarehouse.Application.DTOs;
using DataWarehouse.Application.Services;
using DataWarehouse.Domain;

namespace DataWarehouse.Api.Handlers;

public static class InventoryBatchHandler
{
    public static async Task<IResult> GetAll(InventoryBatchService service)
    {
        var batches = await service.GetAllAsync();
        return Results.Ok(batches);
    }

    public static async Task<IResult> GetByBatchId (Guid batchId, InventoryBatchService service)
    {
        var batch = await service.GetByIdAsync(batchId);
        return batch is not null ? Results.Ok(batch) : Results.NotFound();
    }

    public static async Task<IResult> Add(InventoryBatchRequest request, InventoryBatchService service)
    {
        if(!Enum.TryParse<BatchStatus>(request.Status, ignoreCase: true, out var status))
        return Results.BadRequest("Invalid status. Use: Available, Empty, or Expired.");

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
        return Results.Ok(result);
    }
}