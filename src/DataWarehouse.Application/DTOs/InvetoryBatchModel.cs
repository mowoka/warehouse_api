namespace DataWarehouse.Application.DTOs;

public record InventoryBatchModel(
    ProductModel? Product,
    UserPicModel? PicIn,
    int Id,
    Guid BatchId,
    int Quantity,
    int RemainingQty,
    DateTime EntryDate,
    string Status
);