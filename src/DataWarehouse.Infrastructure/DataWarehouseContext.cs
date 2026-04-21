using Microsoft.EntityFrameworkCore;
using DataWarehouse.Domain;

namespace DataWarehouse.Infrastructure;

public class DataWarehouseContext: DbContext
{
    public DataWarehouseContext(DbContextOptions<DataWarehouseContext> options): base(options){}
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
}
