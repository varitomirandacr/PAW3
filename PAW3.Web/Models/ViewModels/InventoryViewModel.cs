namespace PAW3.Web.Models.ViewModels;

public class InventoryViewModel
{
    public int InventoryId { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? UnitsInStock { get; set; }
    public DateTime? LastUpdated { get; set; }
    public int? ProductId { get; set; }
    public DateTime? DateAdded { get; set; }
    public string? ModifiedBy { get; set; }
}

