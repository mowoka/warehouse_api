using DataWarehouse.Application.Interfaces;

namespace DataWarehouse.Infrastructure;

public class PasswordHasher: IPasswordHasher
{
    private const int WorkFactor = 10;
    
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, WorkFactor);
    }

    public bool Verify(string password, string hashPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);
    }
}