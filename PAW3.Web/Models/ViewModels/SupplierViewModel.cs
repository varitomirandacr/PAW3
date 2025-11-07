namespace PAW3.Web.Models.ViewModels;

public class SupplierViewModel
{
    public int SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? ContactName { get; set; }
    public string? ContactTitle { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public DateTime? LastModified { get; set; }
    public string? ModifiedBy { get; set; }
}

