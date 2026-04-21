using DataWarehouse.Application.DTOs;
using DataWarehouse.Application.Services;

namespace DataWarehouse.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/login", async (LoginRequest request, UserService service) =>
        {
            var token = await service.LoginAsync(request.Email, request.Password);
            if(token == null) return Results.Unauthorized();
            return Results.Ok(new { Token = token });
            
        })
        .WithName("Login")
        .WithTags("Auth");

        app.MapGet("/users", async (UserService service) =>
        {
            var users = await service.GetAllAsync();
            return Results.Ok(users);
        })
        .WithName("GetAllUsers")
        .WithTags("Users");
    }
}