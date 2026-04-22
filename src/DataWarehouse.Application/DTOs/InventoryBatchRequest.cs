namespace DataWarehouse.Application.DTOs;

public record InventoryBatchRequest(
    int ProductId,
    int Quantity,
    int RemainingQuantity,
    DateTime EntryDate,
    int PicInId,
    string Status
);