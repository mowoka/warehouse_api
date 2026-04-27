using DataWarehouse.Api.Helpers;
using DataWarehouse.Application.DTOs;
using DataWarehouse.Application.Services;
using DataWarehouse.Domain;

namespace DataWarehouse.Api.Handlers;


public static class ProductHandler
{
    public static async Task<IResult> GetProducts([AsParameters] PageRequest request,ProductService service)
    {
        var products = await service.GetAllProductsAsync(request.Skip, request.Take);
        var totalProducts = await service.CountTotalProductsAsync();

        var paginationMeta = new PaginationMeta(
            Page : request.page,
            PageSize : request.pageSize,
            TotalCount : totalProducts
        );

        return Results.Ok(PagedApiResponse<IEnumerable<Product>>.Ok(products, paginationMeta,"Get Products Successful"));
    }

    public static async Task<IResult> GetProductById(int id, ProductService service)
    {
        var product = await service.GetProductByIdAsync(id);
        if(product is null)
            return Results.NotFound(ApiResponse<object>.Fail("Product not found"));

        return Results.Ok(ApiResponse<object>.Ok(product, "Get Product Successful"));   
    }

    public static async Task<IResult> AddProduct(ProductRequest request, ProductService service)
    {
        var emptyFields = ValidationHelper.GetEmptyFields(
            ("sku", request.Sku),
            ("productName", request.ProductName),
            ("category", request.Category)
        );

        if(emptyFields.Count > 0)
            return Results.BadRequest(ApiResponse<object>.Fail($"The following fields are required: {string.Join(", ", emptyFields)}"));

        var body = new Product(){
            Sku = request.Sku,
            ProductName = request.ProductName,
            Category = request.Category
        };
        
        var product = await service.AddProductAsync(body);

        return Results.Ok(ApiResponse<Product>.Ok(product, "Add Product Successful"));
    }

    public static async Task<IResult> UpdateProduct(int id, ProductUpdateRequest request, ProductService service)
    {
        var existingProduct = await service.GetProductByIdAsync(id);
        if(existingProduct is null)
            return Results.NotFound(ApiResponse<object>.Fail("Product not found"));

        existingProduct.Sku = request.Sku;
        existingProduct.ProductName = request.ProductName;
        existingProduct.Category = request.Category;

        await service.UpdateProductAsync(existingProduct);
        
        return Results.Ok(ApiResponse<object>.Ok("Update Product Successful"));
    }
}