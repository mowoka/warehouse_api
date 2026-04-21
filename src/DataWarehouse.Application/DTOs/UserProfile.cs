namespace DataWarehouse.Application.DTOs;
public record UserProfile(int UserId, string Name, string Email,string Role ,bool IsActive, string? LastLogin);