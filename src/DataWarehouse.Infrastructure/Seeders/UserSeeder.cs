using DataWarehouse.Application.Interfaces;
using DataWarehouse.Domain;

namespace DataWarehouse.Infrastructure.Seeders;

public class UserSeeder : ISeeder
{
    private readonly IPasswordHasher _passwordHasher;
    public UserSeeder(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }
    public async Task SeedAsync(DataWarehouseContext context)
    {
        if(context.Users.Any()) return;

        var users = new List<User>
        {
            new(){Email = "giomowoka@example.com", Name ="Gio Mowoka" , Role = "Admin" , Password = _passwordHasher.Hash("mowoka123"), IsActive = true, },
            new(){Email = "staff_01@example.com", Name ="Staff Member 01" , Role = "Staff" , Password = _passwordHasher.Hash("mowoka123"), IsActive = true, },
            new(){Email = "manager_01@example.com", Name ="Manager Member 01" , Role = "Manager" , Password = _passwordHasher.Hash("mowoka123"), IsActive = true, },
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }
}