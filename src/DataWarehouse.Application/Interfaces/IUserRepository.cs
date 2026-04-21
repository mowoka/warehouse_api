
using DataWarehouse.Domain;

namespace DataWarehouse.Application.Interfaces;

public interface IUserRepository
{
  Task<IEnumerable<User>> GetAllAsync();
  Task<User?> GetByIdAsync(int id);
  Task AddAsync(User user);   
}