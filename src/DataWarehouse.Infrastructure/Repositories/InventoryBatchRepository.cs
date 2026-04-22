using DataWarehouse.Application.Interfaces;
using DataWarehouse.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouse.Infrastructure.Repositories;

public class InventoryBatchRepository : IInventoryBatchRepository
{
    private readonly DataWarehouseContext _context;

    public InventoryBatchRepository(DataWarehouseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InventoryBatch>> GetAllAsync()
    {
        return await _context.InventoryBatches
        .Include(b => b.product)
        .Include(b => b.PicIn)
        .ToListAsync();
    }

    public async Task<InventoryBatch?> GetByIdAsync(Guid batchId)
    {
        return await _context.InventoryBatches
        .Include(b => b.product)
        .Include(b => b.PicIn)
        .FirstOrDefaultAsync(b => b.BatchId == batchId);
    }

    public async Task<InventoryBatch> AddAsync(InventoryBatch inventory)
    {
        _context.InventoryBatches.Add(inventory);
        await _context.SaveChangesAsync();
        return inventory;
    }
}