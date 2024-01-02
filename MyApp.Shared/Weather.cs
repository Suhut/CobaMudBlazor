using System.Text;

namespace MyApp.Shared;

public class GridDataRequestDto
{
    public int Page { get; set; } = 0; // The page number for the data we're requesting
    public int PageSize { get; set; } = 10; // The number of items per page
}

public class GridDataRequestDtov03
{
    public int Page { get; set; } = 0; // The page number for the data we're requesting
    public int PageSize { get; set; } = 10; // The number of items per page
    public List<GridDataRequestSortDefinitionsv03>? SortDefinitions { get; set; }
    public List<GridDataRequestFilterDefinitionsv03>? FilterDefinitions { get; set; }
}

public class GridDataRequestSortDefinitionsv03
{
    public string SortBy { get; set; }
    public bool Descending { get; set; }
}

public class GridDataRequestFilterDefinitionsv03
{
    public string FieldName { get; set; }
    public string FieldType { get; set; }
    public string Operator { get; set; }
    public string Value { get; set; }
}

public class GridDataRequestUtils03
{
    public string SortDefinitionsToSql(List<GridDataRequestSortDefinitionsv03>? SortDefinitions)
    {
        StringBuilder result = new StringBuilder();
        foreach (var item in SortDefinitions ?? [])
        {
            var fieldName = item.SortBy;

            var statement = item.Descending switch
            {
                true => $"{fieldName} DESC",
                false => $"{fieldName} ASC"
            };

            if (!string.IsNullOrEmpty(result.ToString()))
            {
                result = result.Append(", ");
            }
            result = result.Append(statement);
        }
        return result.ToString();
    }

    public string FilterDefinitionsToSql(List<GridDataRequestFilterDefinitionsv03>? FilterDefinitions)
    {
        StringBuilder result = new StringBuilder();

        foreach (var item in FilterDefinitions ?? [])
        {
            var fieldName = item.FieldName;
            var fieldType = item.FieldType;
            var fieldValue = item.Value?.Replace("'", "''");

            var statement = string.Empty;
            if (fieldType == "String")
            {
                statement = item.Operator switch
                {
                    "contains" => $"{fieldName} LIKE '%{fieldValue}%'",
                    "not contains" => $"NOT({fieldName} LIKE '%{fieldValue}%')",
                    "equals" => $"{fieldName} = '{fieldValue}'",
                    "not equals" => $"{fieldName} != '{fieldValue}'",
                    "starts with" => $"{fieldName} LIKE '%{fieldValue}'",
                    "ends with" => $"{fieldName} LIKE '{fieldValue}%'",
                    "is empty" => $"ISNULL({fieldName},'') = ''",
                    "is not empty" => $"ISNULL({fieldName},'') != ''",
                    _ => string.Empty
                };
            }
            else if (fieldType == "Number")
            {
                statement = item.Operator switch
                {
                    "=" => $"{fieldName} = {fieldValue}",
                    "!=" => $"{fieldName} != {fieldValue}",
                    ">" => $"{fieldName} > {fieldValue}",
                    ">=" => $"{fieldName} >= {fieldValue}",
                    "<" => $"{fieldName} < {fieldValue}",
                    "<=" => $"{fieldName} <= {fieldValue}",
                    "is empty" => $"{fieldName} IS NULL",
                    "is not empty" => $"{fieldName} IS NOT NULL",
                    _ => string.Empty
                };
            }
            else if (fieldType == "DateTime")
            {
                statement = item.Operator switch
                {
                    "is" => $"{fieldName} = '{fieldValue}'",
                    "is not" => $"{fieldName} != '{fieldValue}'",
                    "is after" => $"{fieldName} > '{fieldValue}'",
                    "is on or after" => $"{fieldName} >= '{fieldValue}'",
                    "is before" => $"{fieldName} < '{fieldValue}'",
                    "is on or before" => $"{fieldName} <= '{fieldValue}'",
                    "is empty" => $"{fieldName} IS NULL",
                    "is not empty" => $"{fieldName} IS NOT NULL",
                    _ => string.Empty
                };
            }
            else if (fieldType == "Guid")
            {

                statement = item.Operator switch
                {
                    "equals" => $"{fieldName} = '{fieldValue}'",
                    "not equals" => $"{fieldName} != '{fieldValue}'",
                    _ => string.Empty
                };
            }
            else if (fieldType == "Boolean")
            {
                statement = item.Operator switch
                {
                    "is" => $"{fieldName} = {fieldValue}",
                    _ => string.Empty
                };
            }
            else if (fieldType == "Enum")
            {
                statement = item.Operator switch
                {
                    "is" => $"{fieldName} = '{fieldValue}'",
                    "is not" => $"{fieldName} != '{fieldValue}'",
                    _ => string.Empty
                };
            }


            if (!string.IsNullOrEmpty(statement))
            {
                if (!string.IsNullOrEmpty(result.ToString()))
                {
                    result = result.Append(" AND ");
                }
                result = result.Append(statement);
            }

        }
        return result.ToString();
    }

    //public string ConvertToLinq(GridDataRequestDtov03 request)
    //{

    //}
}




public class WeatherListDto
{
    public List<WeatherListItemDto> Items { get; set; } = new();
    public int ItemTotalCount { get; set; } = 0; // The total count of items before paging
}

public class WeatherListItemDto
{
    public DateTime? Date { get; set; }
    public int? TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int? TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}