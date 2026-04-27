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

    public async Task<IEnumerable<Product>> GetAllAsync(int skip, int take)
        => await _context.Products
        .OrderByDescending(p => p.Id)
        .Skip(skip)
        .Take(take)
        .ToListAsync();
    
    public async Task<int> CountTotalProductsAsync()
        => await _context.Products.CountAsync();
    public async Task<Product?> GetByIdAsync(int id)
        => await _context.Products.FindAsync(id);

    public async Task<Product> AddAsync(Product product)
    {
        var result = await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
}
