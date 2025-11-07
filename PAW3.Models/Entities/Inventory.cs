using System;
using System.Collections.Generic;

namespace PAW3.Models.Entities;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public decimal? UnitPrice { get; set; }

    public int? UnitsInStock { get; set; }

    public DateTime? LastUpdated { get; set; }

    public int? ProductId { get; set; }

    public DateTime? DateAdded { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
