using DataWarehouse.Application.Interfaces;
using DataWarehouse.Domain;
using DataWarehouse.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly DataWarehouseContext _context;

    public UserRepository(DataWarehouseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _context.Users.ToListAsync();
    
    public async Task<User?> GetByIdAsync(int id)
        => await _context.Users.FindAsync(id);
    
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task UpdateLastLoginAsync(User user)
    {
        user.LastLogin = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}