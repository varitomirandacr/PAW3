using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetDataAsync();
}

public class ProductService(IRestProvider restProvider, IConfiguration configuration) : IService<ProductDTO>, IProductService
{
    public async Task<IEnumerable<ProductDTO>> GetDataAsync()
    {
        var url = configuration.GetStringFromAppSettings("APIS", "Product");
        var response = await restProvider.GetAsync(url, null);
        return await JsonProvider.DeserializeAsync<IEnumerable<ProductDTO>>(response);
    }

}
