using System;
using System.Collections.Generic;

namespace PAW3.Models.Entities;

public partial class Notification
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Message { get; set; } = null!;

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }
}
