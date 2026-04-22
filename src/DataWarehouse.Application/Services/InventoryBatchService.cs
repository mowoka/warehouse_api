using DataWarehouse.Application.Interfaces;
using DataWarehouse.Domain;

namespace DataWarehouse.Application.Services;

public class InventoryBatchService
{
    private readonly IInventoryBatchRepository _repository;

    public InventoryBatchService(IInventoryBatchRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<InventoryBatch>> GetAllAsync()
        => _repository.GetAllAsync();
    
    public Task<InventoryBatch?> GetByIdAsync(Guid batchId)
        => _repository.GetByIdAsync(batchId);

    public Task<InventoryBatch> AddAsync(InventoryBatch batch)
        => _repository.AddAsync(batch);
}