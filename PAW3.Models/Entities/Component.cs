using System;
using System.Collections.Generic;

namespace PAW3.Models.Entities;

public partial class Component
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;
}
