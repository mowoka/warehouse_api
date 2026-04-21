namespace DataWarehouse.Infrastructure.Seeders;

public class DatabaseSeeder
{
    private readonly DataWarehouseContext _context;
    private readonly IEnumerable<ISeeder> _seeders;

    public DatabaseSeeder(DataWarehouseContext context, IEnumerable<ISeeder> seeders)
    {
        _context = context;
        _seeders = seeders;
    }

    public async Task SeedAsync()
    {
        foreach (var seeder in _seeders)
        {
            await seeder.SeedAsync(_context);
        }
    }
}
