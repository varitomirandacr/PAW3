using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.ServiceLocator.Helper;

namespace PAW3.Mvc.ServiceLocator;

public interface IServiceLocatorService
{
    Task<IEnumerable<T>> GetDataAsync<T>(string name);
}

public class ServiceLocatorService(IRestProvider restProvider, IServiceMapper serviceMapper) : IServiceLocatorService
{
    public async Task<IEnumerable<T>> GetDataAsync<T>(string name)
    {
        var response = await restProvider.GetAsync("https://localhost:7130/api/ServiceLocator/", name);
        return await JsonProvider.DeserializeAsync<IEnumerable<T>>(response);
    }
}
