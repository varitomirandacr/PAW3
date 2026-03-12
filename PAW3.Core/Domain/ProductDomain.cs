using PAW3.Models.Entities.Productdb;

namespace PAW3.Core.Domain;

public class ProductDomain(Product product)
{
    private readonly Product _product = product;
    public Product Product => _product;

    public ProductDomain CleanRating()
    {
        _product.Rating ??= 3;
        return this;
    }

    public ProductDomain ApplyRatingClass()
    {
        bool isHigherClass = _product.Rating <= 5 && _product.Rating > 4.5M;
        bool isMidClass = _product.Rating <= 4.5M && _product.Rating >= 2.5M;
        _product.RatingClass = isHigherClass 
            ? "A" 
            : isMidClass
                ? "B" : "C";
        return this;
    }

    public ProductDomain ApplyTimeClass() 
    {
        bool isHighClass = _product.LastModified.Value >= _product.LastModified.Value.AddDays(-15);
        bool isMidClass = _product.LastModified.Value >= _product.LastModified.Value.AddDays(-5);

        if (_product.LastModified == null || (!isHighClass && !isMidClass))
        {
            _product.TimeClass = "C";
            return this;
        }

        _product.TimeClass = isHighClass ? "A" : "B";
        return this;        
    }
}
