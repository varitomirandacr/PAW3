using System;
using System.Collections.Generic;

namespace PAW3.Data.Models;

public partial class Component
{
    public decimal Id { get; set; }

    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;
}
