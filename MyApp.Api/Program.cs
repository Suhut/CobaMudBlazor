using MudBlazor;
using MyApp.Shared;
using System.Linq;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("https://localhost:44398")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecastv01", () =>
{
    GridDataRequestDto request = new();
    request.Page = 0;
    request.PageSize = 10;

    WeatherListDto result = new();

    var items = Enumerable.Range(1, 1000).Select(index =>
        new WeatherListItemDto
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }
        )
        .ToList();

    result.ItemTotalCount = items.Count;
    result.Items = items;//.Skip(request.PageSize* request.PageSize).Take(request.PageSize).ToList();

    return result;
})
.WithName("GetWeatherForecastv01")
.WithOpenApi();

app.MapPost("/weatherforecastv02", (GridDataRequestDto request) =>
{ 

    WeatherListDto result = new();

    var items = Enumerable.Range(1, 1000).Select(index =>
        new WeatherListItemDto
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }
        )
        .ToList();

    result.ItemTotalCount = items.Count;
    int pageNumber = request.Page;
    int pageSize = request.PageSize;

    result.Items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

    return result;
})
.WithName("GetWeatherForecastv02")
.WithOpenApi();

app.MapPost("/weatherforecastv03", (GridDataRequestDtov03 request) =>
{

    WeatherListDto result = new();

    var items = Enumerable.Range(1, 1000).Select(index =>
        new WeatherListItemDto
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }
        )
        .ToList();

    result.ItemTotalCount = items.Count;
    int pageNumber = request.Page;
    int pageSize = request.PageSize;

    result.Items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

    return result;
})
.WithName("GetWeatherForecastv03")
.WithOpenApi();



app.UseCors("default");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
