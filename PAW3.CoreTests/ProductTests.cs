using Moq;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Repositories;
using PAW3.Models.DTO;
using Shouldly;
using PAW3.Architecture.Extensions;
using PAW3.Models.Entities.Productdb;

namespace PAW3.CoreTests;

public class ProductTests
{
    private readonly IEnumerable<Product > products =
    [
        new Product { ProductId = 1, ProductName = "A", Rating = 5 },
        new Product { ProductId = 2, ProductName = "B", Rating = 5 },
        new Product { ProductId = 3, ProductName = "C", Rating = 4 }
    ];

    private readonly List<ProductSummary> expectedSummaries =
    [
        new ProductSummary { Rating = 5, Count = 2 },
        new ProductSummary { Rating = 4, Count = 1 },
        new ProductSummary { Rating = 3, Count = 1 }
    ];

    private readonly Mock<IRepositoryProduct> _repositoryProductMock = new();
    private readonly Mock<IProductBusiness> _productBusinessMock = new();
    private readonly IProductBusiness _business;

    public ProductTests()
    {
        _repositoryProductMock = new Mock<IRepositoryProduct>();
        _productBusinessMock = new Mock<IProductBusiness>();
        _business = new ProductBusiness(_repositoryProductMock.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetProducts_WhenIdIsNull_ShouldGroupAndSummarize()
    {
        // Arrange
        var fakeProductDto = new ProductDTO
        {
            Products = this.products,
            Summaries = this.expectedSummaries
        };

        _repositoryProductMock
            .Setup(rp => rp.ReadAsync())
            .ReturnsAsync(this.products);

        _productBusinessMock
            .Setup(pb => pb.GetProducts(null))
            .ReturnsAsync(fakeProductDto);

        // Act
        var result = await _business.GetProducts(null);

        // Assert
        _repositoryProductMock.Verify(rp => rp.ReadAsync(), Times.Once);
        _repositoryProductMock.Verify(rp => rp.FindAsync(It.IsAny<int>()), Times.Never);
                
        //Should.Equals(result.Products.Count(), this.products.Count());

        Assert.NotNull(result);
        Assert.True(result.Products.Count() == this.products.Count());
        Assert.NotEmpty(result.Summaries);
        Assert.Equal(this.expectedSummaries.Count(), result.Summaries.Count);
        //Assert.Equal(2, result.Summaries.FirstOrDefault().Count);
    }

    [Fact]
    public void GetId_WhenCallingGenerateIdFromNow_ShouldRecieveGeneratedId()
    {
        // Arrange 
        var dateTimeNow = DateTime.Now;

        // Act
        var generatedId = dateTimeNow.GenerateIdFromNow();

        // Assert
        Assert.True(generatedId > 0);
        Assert.IsAssignableFrom<int>(generatedId);
        Assert.InRange(generatedId, 0, int.MaxValue);
    }
}
