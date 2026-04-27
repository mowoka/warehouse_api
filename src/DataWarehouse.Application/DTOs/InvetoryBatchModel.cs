using DataWarehouse.Domain;

namespace DataWarehouse.Application.DTOs;

public record InventoryBatchModel(
    ProductModel? product,
    UserPicModel? PicIn,
    int id,
    Guid batchId,
    int quantity,
    int remainingQty,
    DateTime entryDate,
    BatchStatus Status
);