namespace DataWarehouse.Application.DTOs;

public record InventoryBatchRequest(
    int ProductId,
    int Quantity,
    int RemainingQuantity,
    int PicInId,
    string Status
);