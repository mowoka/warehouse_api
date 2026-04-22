using Microsoft.EntityFrameworkCore;
using DataWarehouse.Domain;

namespace DataWarehouse.Infrastructure;

public class DataWarehouseContext: DbContext
{
    public DataWarehouseContext(DbContextOptions<DataWarehouseContext> options): base(options){}
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<InventoryBatch> InventoryBatches => Set<InventoryBatch>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Store the enum as string in the DB
        modelBuilder.Entity<InventoryBatch>()
            .Property(b => b.Status)
            .HasConversion<string>();

        // Tell EF Core: ProductId => Products Table
        modelBuilder.Entity<InventoryBatch>()
            .HasOne(b => b.product)
            .WithMany()
            .HasForeignKey(b => b.ProductId);

        // Tell EF Core: PicInID => Users table
        modelBuilder.Entity<InventoryBatch>()
            .HasOne(b => b.PicIn)
            .WithMany()
            .HasForeignKey(b => b.PicInId);
    }
}
