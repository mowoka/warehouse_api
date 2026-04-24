using System.Text.Json.Serialization;
using DataWarehouse.Application.DTOs;

public class PagedApiResponse<T> : ApiResponse<T>
{
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PaginationMeta Pagination { get; set; } = default!;

    public static PagedApiResponse<T> Ok(T data, PaginationMeta pagination, string message = "")
    {
        return new PagedApiResponse<T>
        {
            Sucess = true,
            Message = message,
            Data = data,
            Pagination = pagination
        };
    }
}