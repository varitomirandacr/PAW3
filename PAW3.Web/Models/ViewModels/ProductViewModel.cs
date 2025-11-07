namespace PAW3.Web.Models.ViewModels;

public class ProductDtoViewModel
{
    public IEnumerable<ProductViewModel> Products { get; set; } = [];
    public List<ProductSummaryViewModel> Summaries { get; set; } = [];
}

public class ProductSummaryViewModel
{
    public decimal? Id { get; set; }
    public string? Name { get; set; }
    public decimal? Rating { get; set; }
    public int Count { get; set; }
}

public class ProductViewModel
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public int? InventoryId { get; set; }
    public int? SupplierId { get; set; }
    public string? Description { get; set; }
    public decimal? Rating { get; set; }
    public int? CategoryId { get; set; }
    public DateTime? LastModified { get; set; }
    public string? ModifiedBy { get; set; }
}

