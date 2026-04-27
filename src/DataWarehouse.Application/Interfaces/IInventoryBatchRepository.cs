using DataWarehouse.Domain;

namespace DataWarehouse.Application.Interfaces;

public interface IInventoryBatchRepository
{
    Task<IEnumerable<InventoryBatch>> GetAllAsync(int skip, int take);
    Task<int> CountTotalAsync();
    Task<InventoryBatch?> GetByIdAsync(Guid batchId);
    Task<InventoryBatch> AddAsync(InventoryBatch batch);
}