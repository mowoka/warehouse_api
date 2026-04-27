namespace DataWarehouse.Api.Helpers;

public static class ValidationHelper
{
    public static List<string> GetEmptyFields(params (string FieldName, string? value)[] fields)
    {
        return fields
            .Where(f => string.IsNullOrWhiteSpace(f.value))
            .Select(f => f.FieldName)
            .ToList();
    }
}