
using DataWarehouse.Domain;

namespace DataWarehouse.Application.Interfaces;

public interface IUserRepository
{
  Task<IEnumerable<User>> GetAllAsync(int skip, int take);

  Task<int> CountTotalUsersAsync();
  Task<User?> GetByIdAsync(int id);
  Task AddAsync(User user);
  Task<User?> GetByEmailAsync(string email);
  Task UpdateLastLoginAsync(User user);
  Task updateAsync(User user);   
}