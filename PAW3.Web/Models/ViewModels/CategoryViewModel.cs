namespace PAW3.Web.Models.ViewModels;

public class CategoryViewModel
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
    public DateTime? LastModified { get; set; }
    public string? ModifiedBy { get; set; }
}

