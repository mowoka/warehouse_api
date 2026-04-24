using DataWarehouse.Api.Handlers;

namespace DataWarehouse.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {

        app.MapPost("/auth/login", UserHandler.Login)
            .WithName("Login")
            .WithTags("Auth");

        app.MapPost("/auth/logout", UserHandler.Logout)
            .WithName("Logout")
            .WithTags("Auth")
            .RequireAuthorization();

        app.MapGet("/me", UserHandler.GetProfile)
            .WithName("GetProfile")
            .WithTags("Profile")
            .RequireAuthorization();

        app.MapGet("/users", UserHandler.GetAllUsers)
            .WithName("GetAllUsers")
            .WithTags("Users")
            .RequireAuthorization("AdminOnly");

        app.MapPut("/users/{id}", UserHandler.UpdateUser)
            .WithName("UpdateUser")
            .WithTags("Users")
            .RequireAuthorization("AdminOnly");

        app.MapPost("/users", UserHandler.CreateUser)
            .WithName("CreateUser")
            .WithTags("Users")
            .RequireAuthorization("AdminOnly");
    }
}