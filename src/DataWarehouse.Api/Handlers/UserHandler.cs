using System.Security.Claims;
using DataWarehouse.Api.Helpers;
using DataWarehouse.Application.DTOs;
using DataWarehouse.Application.Services;
using DataWarehouse.Domain;

namespace DataWarehouse.Api.Handlers;

public static class UserHandler
{
    public static async Task<IResult> Login(LoginRequest request, UserService service)
    {
        var emptyFields = ValidationHelper.GetEmptyFields(
            ("Email", request.Email),
            ("Password", request.Password)
        );

        if(emptyFields.Count > 0)
            return Results.BadRequest(ApiResponse<object>.Fail($"The following fields are required: {string.Join(", ", emptyFields)}"));

        var token = await service.LoginAsync(request.Email, request.Password);
        if (token is null) return Results.Unauthorized();
        return Results.Ok(ApiResponse<LoginResponse>.Ok(new LoginResponse(token), "Login Successful"));
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

        return Results.Ok(PagedApiResponse<IEnumerable<UserModel>>.Ok(userProfiles, paginationMeta, "Get All Users Successful"));
    }

    public static async Task<IResult> UpdateUser(int id, UserUpdateRequest request, UserService service)
    {
        var user  = await service.GetUserByIdAsync(id);
        if(user is null) return Results.NotFound(ApiResponse<object>.Fail("User not found"));
        
        user.Name = request.Name;
        user.Role = request.Role;
        if (!string.IsNullOrEmpty(request.Password))
        {
            user.Password=  request.Password;
        }
        user.IsActive = request.IsActive;

        await service.UpdateUserAsync(user);   
        return Results.Ok(ApiResponse<object>.Ok("User updated successfully"));
    }

    public static async Task<IResult> CreateUser(UserCreateRequest request,UserService service)
    {
        var findUser = await service.GetUserByEmailAsync(request.Email);
        if(findUser is not null)
            return Results.BadRequest(ApiResponse<object>.Fail("Email already in use"));

        var emptyFields = ValidationHelper.GetEmptyFields(
            ("Name", request.Name),
            ("Email", request.Email),
            ("Password", request.Password),
            ("Role", request.Role),
            ("IsActive", request.IsActive.ToString())
        );
        
        if(emptyFields.Count > 0)
            return Results.BadRequest(ApiResponse<object>.Fail($"The following fields are required: {string.Join(", ", emptyFields)}"));

        var newUser = new User()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role,
            IsActive = request.IsActive
        };

        await service.AddUserAsync(newUser);
        
        return Results.Ok(ApiResponse<object>.Ok("User created successfully"));
    }
}