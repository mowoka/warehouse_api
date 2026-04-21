using DataWarehouse.Domain;

namespace DataWarehouse.Infrastructure.Seeders;

public class ProductSeeder : ISeeder
{
    public async Task SeedAsync(DataWarehouseContext context)
    {
        if (context.Products.Any()) return;

        var products = new List<Product>
        {
            new() { Sku = "SKU-001", ProductName = "Laptop Pro", Category = "Electronics" },
            new() { Sku = "SKU-002", ProductName = "Mechanical Keyboard", Category = "Electronics" },
            new() { Sku = "SKU-003", ProductName = "Office Chair", Category = "Furniture" },
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
