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

    public Task<IEnumerable<Product>> GetAllProductsAsync()
        => _repository.GetAllAsync();

    public Task<Product?> GetProductByIdAsync(int id)
        => _repository.GetByIdAsync(id);

    public Task AddProductAsync(Product product)
        => _repository.AddAsync(product);
}
