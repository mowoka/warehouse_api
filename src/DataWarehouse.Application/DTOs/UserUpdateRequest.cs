namespace DataWarehouse.Application.DTOs;

public record UserUpdateRequest(
    string Name,
    string Password,
    string Role,
    bool IsActive
);