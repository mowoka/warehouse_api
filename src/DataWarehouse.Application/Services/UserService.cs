
using DataWarehouse.Application.Interfaces;
using DataWarehouse.Domain;

namespace DataWarehouse.Application.Services;

public class UserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public UserService(IUserRepository repository, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public Task<IEnumerable<User>> GetAllAsync(int skip, int take)
        => _repository.GetAllAsync(skip, take);

    public Task<int> CountTotalUsersAsync()
        => _repository.CountTotalUsersAsync();

    public Task<User?> GetUserByIdAsync(int id)
        => _repository.GetByIdAsync(id);
    public Task<User?> GetUserByEmailAsync(string email)
        => _repository.GetByEmailAsync(email);
    public Task AddUserAsync(User user)
    {
        user.Password = _passwordHasher.Hash(user.Password);
        return _repository.AddAsync(user);
    }
    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _repository.GetByEmailAsync(email);
        if(user is null || !user.IsActive) return null;
        if(!_passwordHasher.Verify(password, user.Password)) return null;

        await _repository.UpdateLastLoginAsync(user);
        return _jwtTokenService.GenerateToken(user);
    }

    public Task UpdateUserAsync(User user)
    {
        user.Password = _passwordHasher.Hash(user.Password);
        return _repository.UpdateAsync(user);
    }
}