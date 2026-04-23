using System.Security.Claims;
using DataWarehouse.Application.DTOs;
using DataWarehouse.Application.Services;

namespace DataWarehouse.Api.Handlers;

public static class UserHandler
{
    public static async Task<IResult> Login(LoginRequest request, UserService service)
    {
        var token = await service.LoginAsync(request.Email, request.Password);
        if(token == null) return Results.Unauthorized();
        return Results.Ok(ApiResponse<object>.Ok(new {token}, "Login Successful"));
    }

    public static async Task<IResult> Logout()
    {
        return Results.Ok();
    }

    public static async Task<IResult> GetProfile(ClaimsPrincipal user, UserService service)
    {
        var userIdStr = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if(userIdStr is null) return Results.Unauthorized();
        var userProfile = await service.GetUserByIdAsync(int.Parse(userIdStr));
        if(userProfile is null) return Results.NotFound();
            
        var profile = new UserModel(
            UserId: userProfile.UserId,
            Name: userProfile.Name,
            Email: userProfile.Email,
            Role: userProfile.Role,
            IsActive: userProfile.IsActive,
            LastLogin: userProfile.LastLogin.ToString()   
        );

    return Results.Ok(ApiResponse<object>.Ok(profile, "Get Profile Successful"));
    }

    public static async Task<IResult> GetAllUsers([AsParameters] PageRequest request,UserService service)
    {
        var users = await service.GetAllAsync(request.Skip, request.Take);
        var totalUsers = await service.CountTotalUsersAsync();

        var userProfiles = users.Select(u => new UserModel(
            UserId: u.UserId,
            Name: u.Name,
            Email: u.Email,
            Role: u.Role,
            IsActive: u.IsActive, 
            LastLogin: u.LastLogin.ToString()
        )).ToList();

        var paginationMeta = new PaginationMeta(
            Page: request.page,
            PageSize: request.pageSize,
            TotalCount: totalUsers
        );

        return Results.Ok(ApiResponse<IEnumerable<UserModel>>.Ok(userProfiles, paginationMeta, "Get All Users Successful"));
    }
}