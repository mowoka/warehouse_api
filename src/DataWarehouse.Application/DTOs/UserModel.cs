namespace DataWarehouse.Application.DTOs;
public record UserModel(int UserId, string Name, string Email,string Role ,bool IsActive, string? LastLogin);
