namespace DataWarehouse.Domain;

public class Product
{
    public int Id { get; set; }
    public required string Sku { get; set; }
    public required string ProductName { get; set; }
    public required string Category { get; set; }
}
