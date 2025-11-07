namespace PAW3.Web.Models.ViewModels;

public class TaskViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
    public string? ModifiedBy { get; set; }
}

