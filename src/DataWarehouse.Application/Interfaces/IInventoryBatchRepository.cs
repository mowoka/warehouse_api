using DataWarehouse.Domain;

namespace DataWarehouse.Application.Interfaces;

public interface IInventoryBatchRepository
{
    Task<IEnumerable<InventoryBatch>> GetAllAsync();
    Task<InventoryBatch?> GetByIdAsync(Guid batchId);
    Task<InventoryBatch> AddAsync(InventoryBatch batch);
}