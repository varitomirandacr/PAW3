namespace PAW3.Web.Models.ViewModels;

public class NotificationViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; } = null!;
    public bool? IsRead { get; set; }
    public DateTime? CreatedAt { get; set; }
}

