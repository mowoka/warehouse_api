using DataWarehouse.Application.DTOs;
using DataWarehouse.Application.Services;
using DataWarehouse.Domain;

namespace DataWarehouse.Api.Handlers;

public static class InventoryBatchHandler
{
    public static async Task<IResult> GetAll([AsParameters] PageRequest request, InventoryBatchService service)
    {

        var batches = await service.GetAllAsync(request.Skip, request.Take);
        var totalBatches = await service.CountTotalAsync();

        var paginationMeta = new PaginationMeta(
            Page: request.page,
            PageSize: request.pageSize,
            TotalCount: totalBatches
        );

        var result = batches.Select(b => new InventoryBatchModel(
            product: new ProductModel(b.product!.Id, b.product.Sku, b.product.ProductName, b.product.Category),
            PicIn:  new UserPicModel(b.PicIn!.UserId, b.PicIn.Name, b.PicIn.Email, b.PicIn.Role),
            id: b.Id,
            batchId: b.BatchId,
            quantity: b.Quantity,
            remainingQty: b.RemainingQty,
            entryDate: b.EntryDate,
            Status: b.Status
        )).ToList();

        return Results.Ok(PagedApiResponse<IEnumerable<InventoryBatchModel>>.Ok(result, paginationMeta, "Get Inventory Batches Successful"));
    }

    public static async Task<IResult> GetByBatchId (Guid batchId, InventoryBatchService service)
    {
        var batch = await service.GetByIdAsync(batchId);
        if(batch is null)
            return Results.NotFound(ApiResponse<object>.Fail("Inventory Batch not found"));

        var result = new InventoryBatchModel(
            product: new ProductModel(batch.product!.Id, batch.product.Sku, batch.product.ProductName, batch.product.Category),
            PicIn:  new UserPicModel(batch.PicIn!.UserId, batch.PicIn.Name, batch.PicIn.Email, batch.PicIn.Role),
            id: batch.Id,
            batchId: batch.BatchId,
            quantity: batch.Quantity,
            remainingQty: batch.RemainingQty,
            entryDate: batch.EntryDate,
            Status: batch.Status
        );
        
        return Results.Ok(ApiResponse<object>.Ok(result, "Get Inventory Batch Successful"));
    }

    public static async Task<IResult> Add(InventoryBatchRequest request, InventoryBatchService service)
    {
        if(!Enum.TryParse<BatchStatus>(request.Status, ignoreCase: true, out var status))
            return Results.BadRequest(ApiResponse<object>.Fail("Invalid status. Use: Available, Empty, or Expired."));

        var data = new InventoryBatch
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            RemainingQty = request.Quantity,
            PicInId = request.PicInId,
            Status = status,
        };

        var batchData = await service.AddAsync(data);
        var batch = await service.GetByIdAsync(batchData.BatchId);
        
        var result = new InventoryBatchModel(
            product: new ProductModel(batch!.product!.Id, batch.product.Sku, batch.product.ProductName, batch.product.Category),
            PicIn:  new UserPicModel(batch.PicIn!.UserId, batch.PicIn.Name, batch.PicIn.Email, batch.PicIn.Role),
            id: batch.Id,
            batchId: batch.BatchId,
            quantity: batch.Quantity,
            remainingQty: batch.RemainingQty,
            entryDate: batch.EntryDate,
            Status: batch.Status
        );

        return Results.Ok(ApiResponse<InventoryBatchModel>.Ok(result, "Add Inventory Batch Successful"));
    }
}