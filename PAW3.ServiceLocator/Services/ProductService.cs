using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.Entities.Productdb;
using PAW3.ServiceLocator.Extensions;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetDataAsync();
}

public class ProductService(IRestProvider restProvider, IConfiguration configuration) : IService<Product>, IProductService
{
    public async Task<IEnumerable<Product>> GetDataAsync()
    {
        var url = configuration.GetStringFromAppSettings("APIS", "Product");
        var response = await restProvider.GetAsync(url, null);
        return JsonProvider.DeserializeSimple<IEnumerable<Product>>(response) ?? Enumerable.Empty<Product>();
    }

}
