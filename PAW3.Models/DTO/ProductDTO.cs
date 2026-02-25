using PAW3.Models.Entities.Productdb;

namespace PAW3.Models.DTO
{
    public class ProductDTO
    {
        public IEnumerable<Product> Products { get; set; } = [];

        public List<ProductSummary> Summaries { get; set; } = [];
    }
}
