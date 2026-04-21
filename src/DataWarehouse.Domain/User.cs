namespace DataWarehouse.Domain;


public class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string Role { get; set; } = "Staff";
    public bool IsActive { get; set; } = true;
    public DateTime LastLogin { get; set; }
}