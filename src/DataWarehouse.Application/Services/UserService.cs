
using DataWarehouse.Application.Interfaces;
using DataWarehouse.Domain;

namespace DataWarehouse.Application.Services;

public class UserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository repository, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public Task<IEnumerable<User>> GetAllAsync()
        => _repository.GetAllAsync();

    public Task<User?> GetUserByIdAsync(int id)
        => _repository.GetByIdAsync(id);

    public Task AddUserAsync(User user)
    {
        user.Password = _passwordHasher.Hash(user.Password);
        return _repository.AddAsync(user);
    }
}