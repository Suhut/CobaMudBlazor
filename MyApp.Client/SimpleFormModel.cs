namespace MyApp.Client;

public class SimpleFormModel
{
    public int Id { get; set; }
    public string IduTextBox { get; set; } = string.Empty;
    public string IduTextBoxEmail { get; set; } = string.Empty;
    public string? IduTextArea { get; set; }
    public int? IduInt { get; set; }
    public decimal? IduDecimal { get; set; }

}