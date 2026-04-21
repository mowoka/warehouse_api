using DataWarehouse.Application.Interfaces;
using DataWarehouse.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouse.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DataWarehouseContext _context;

    public ProductRepository(DataWarehouseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _context.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(int id)
        => await _context.Products.FindAsync(id);

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }
}
