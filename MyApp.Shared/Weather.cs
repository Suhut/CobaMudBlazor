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
    public string Operator { get; set; }
    public object Value { get; set; }
}




public class WeatherListDto
{
    public List<WeatherListItemDto> Items { get; set; } = new();
    public int ItemTotalCount { get; set; } = 0; // The total count of items before paging
}

public class WeatherListItemDto
{
    public DateOnly Date { get; set; } 
    public int TemperatureC { get; set; } 
    public string? Summary { get; set; } 
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}