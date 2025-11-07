namespace PAW3.Models.DTO;

public class ProductSummary
{
    public decimal? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? Rating { get; set; }
    public int Count { get; set; }
}
