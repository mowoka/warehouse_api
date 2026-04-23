public record UserCreateRequest(
    string Name,
    string Email,
    string Password,
    string Role,
    bool IsActive
);