using System.Text.Json.Serialization;

namespace MyApp.Client.Pages;

public class DataGridCustomFormEditingModel
{
    [JsonPropertyName("date")]
    public DateTime? Tanggal { get; set; }

    public int? TemperatureC { get; set; }

    public string? Summary { get; set; }

    public decimal? TemperatureF => (32.0m + (TemperatureC / 0.5556m));
}