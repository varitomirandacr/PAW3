using System.Text.Json.Serialization;

namespace PAW3.Models.DTOs;

public class ProductDTO
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }
    [JsonPropertyName("productName")]
    public string? ProductName { get; set; }
    [JsonPropertyName("inventoryId")]
    public int? InventoryId { get; set; }
    [JsonPropertyName("supplierId")]
    public int? SupplierId { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("rating")]
    public decimal? Rating { get; set; }
    [JsonPropertyName("categoryId")]
    public int? CategoryId { get; set; }
    [JsonPropertyName("lastModified")]
    public DateTime? LastModified { get; set; }
    [JsonPropertyName("modifiedBy")]
    public string? ModifiedBy { get; set; }
}
