using DataWarehouse.Application.Services;

namespace DataWarehouse.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/users", async (UserService service) =>
        {
            var users = await service.GetAllAsync();
            return Results.Ok(users);
        })
        .WithName("GetAllUsers")
        .WithTags("Users");
    }
}