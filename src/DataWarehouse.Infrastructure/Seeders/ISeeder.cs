namespace DataWarehouse.Infrastructure.Seeders;

public interface ISeeder
{
    Task SeedAsync(DataWarehouseContext context);
}
