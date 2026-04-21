using DataWarehouse.Domain;

namespace DataWarehouse.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}