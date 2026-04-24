using DataWarehouse.Api.Handlers;
using DataWarehouse.Application.DTOs;

namespace DataWarehouse.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {

        app.MapPost("/auth/login", UserHandler.Login)
            .WithName("Login")
            .WithTags("Auth")
            .Produces<ApiResponse<LoginResponse>>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapPost("/auth/logout", UserHandler.Logout)
            .WithName("Logout")
            .WithTags("Auth")
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapGet("/me", UserHandler.GetProfile)
            .WithName("GetProfile")
            .WithTags("Profile")
            .RequireAuthorization()
            .Produces<ApiResponse<UserModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapGet("/users", UserHandler.GetAllUsers)
            .WithName("GetAllUsers")
            .WithTags("Users")
            .RequireAuthorization("AdminOnly")
            .Produces<ApiResponse<IEnumerable<UserModel>>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapPut("/users/{id}", UserHandler.UpdateUser)
            .WithName("UpdateUser")
            .WithTags("Users")
            .RequireAuthorization("AdminOnly")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapPost("/users", UserHandler.CreateUser)
            .WithName("CreateUser")
            .WithTags("Users")
            .RequireAuthorization("AdminOnly")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}