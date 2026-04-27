namespace DataWarehouse.Application.DTOs;

public class ApiErrorResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;

    public static ApiErrorResponse Fail(string message)
    {
        return new ApiErrorResponse
        {
            Success = false,
            Message = message
        };
    }
}