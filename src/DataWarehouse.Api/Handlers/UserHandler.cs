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
        return Results.Ok(new { Token = token });
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
            
        var userProfileDto = new UserProfile(
            UserId: userProfile.UserId,
            Name: userProfile.Name,
            Email: userProfile.Email,
            Role: userProfile.Role,
            IsActive: userProfile.IsActive,
            LastLogin: userProfile.LastLogin.ToString()   
        );

        return Results.Ok(new { userProfile = userProfileDto });
    }
}