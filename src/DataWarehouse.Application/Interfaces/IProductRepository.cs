using DataWarehouse.Domain;

namespace DataWarehouse.Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(int skip, int take);
    Task<int> CountTotalProductsAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<Product?> FindProductBySkuAsync(string sku);
}
