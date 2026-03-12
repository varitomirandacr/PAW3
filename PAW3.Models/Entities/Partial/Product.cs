using System.ComponentModel.DataAnnotations.Schema;

namespace PAW3.Models.Entities.Productdb;

public partial class Product
{
    [NotMapped]
    public string RatingClass { get;set; }
    [NotMapped]
    public string TimeClass { get; set; }

}
