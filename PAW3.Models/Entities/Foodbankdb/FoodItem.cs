using System;
using System.Collections.Generic;

namespace PAW3.Data.Foodbankdb.Models;

public partial class FoodItem
{
    public int FoodItemId { get; set; }

    public string Name { get; set; } = null!;

    public string? Category { get; set; }

    public string? Brand { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? Unit { get; set; }

    public int? QuantityInStock { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public bool? IsPerishable { get; set; }

    public int? CaloriesPerServing { get; set; }

    public string? Ingredients { get; set; }

    public string? Barcode { get; set; }

    public string? Supplier { get; set; }

    public DateTime? DateAdded { get; set; }

    public bool? IsActive { get; set; }

    public int? RoleId { get; set; }
}
