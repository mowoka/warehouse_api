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

    public async Task<IEnumerable<InventoryBatch>> GetAllAsync(int skip, int take)
    {
        return await _context.InventoryBatches
        .Skip(skip)
        .Take(take)
        .OrderByDescending(b => b.EntryDate)
        .Include(b => b.product)
        .Include(b => b.PicIn)
        .ToListAsync();
    }
    public async Task<int> CountTotalAsync()
    {
        return await _context.InventoryBatches.CountAsync();
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