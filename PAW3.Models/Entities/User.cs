using System;
using System.Collections.Generic;

namespace PAW3.Models.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? LastModified { get; set; }

    public string? ModifiedBy { get; set; }

    public int? RoleId { get; set; }
}
