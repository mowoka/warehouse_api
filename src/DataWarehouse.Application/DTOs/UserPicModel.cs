namespace DataWarehouse.Application.DTOs;

public record UserPicModel(
   int UserId,
   string Name,
   string Email,
   string Role
);