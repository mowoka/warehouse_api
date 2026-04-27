using DataWarehouse.Application.Interfaces;
using DataWarehouse.Domain;

namespace DataWarehouse.Application.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Product>> GetAllProductsAsync(int skip, int take)
        => _repository.GetAllAsync(skip, take);
    public Task<int> CountTotalProductsAsync()
        => _repository.CountTotalProductsAsync();
    public Task<Product?> GetProductByIdAsync(int id)
        => _repository.GetByIdAsync(id);
    public Task<Product> AddProductAsync(Product product)
        => _repository.AddAsync(product);
    public Task<Product> UpdateProductAsync(Product product)
        => _repository.UpdateAsync(product);
}
