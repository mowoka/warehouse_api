using System.Text.Json.Serialization;
using DataWarehouse.Domain;

namespace DataWarehouse.Application.DTOs;

public record PaginationMeta(int Page, int PageSize, int TotalCount)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
}

public class ApiResponse<T>
{
    public bool Sucess { get; set; }
    public string Message { get; set; } = string.Empty;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }
    
    public static ApiResponse<T> Ok(string message = "")
    {
        return new ApiResponse<T>
        {
            Sucess = true,
            Message = message,
        };
    }
    public static ApiResponse<T> Ok(T data, string message = "")
    {
        return new ApiResponse<T>
        {
            Sucess = true,
            Message = message,
            Data = data
        };
    }
    public static ApiResponse<T> Fail(string message)
    {
        return new ApiResponse<T>
        {
            Sucess = false,
            Message = message,
            Data = default
        };
    }

    public static ApiResponse<List<Product>>? Ok(IEnumerable<Product> products, string v)
    {
        throw new NotImplementedException();
    }
}

