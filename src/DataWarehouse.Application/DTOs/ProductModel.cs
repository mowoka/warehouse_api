namespace DataWarehouse.Application.DTOs;

public record ProductModel(
    int Id,
    string ProductName,
    string Sku,
    string Category
);