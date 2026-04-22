namespace DataWarehouse.Application.DTOs;

public record ProductRequest(
    string Sku,
    string ProductName,
    string Category
);