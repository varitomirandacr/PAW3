using PAW3.Models.Entities.Productdb;
using PAW3.ServiceLocator.Services.Contracts;
using ModelsTask = PAW3.Models.Entities.Productdb.Task;

namespace PAW3.ServiceLocator.Helper;

public interface IServiceMapper
{
    System.Threading.Tasks.Task<IService<T>> GetServiceAsync<T>(string name);
}

public class ServiceMapper : IServiceMapper
{
    private readonly IServiceProvider serviceProvider;

    public ServiceMapper(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public System.Threading.Tasks.Task<IService<T>> GetServiceAsync<T>(string name)
    {
        var service = name.ToLower() switch
        {
            "product" => (IService<T>)serviceProvider.GetRequiredService<IService<Product>>(),
            //"category" => (IService<T>)serviceProvider.GetRequiredService<IService<Category>>(),
            //"task" => (IService<T>)serviceProvider.GetRequiredService<IService<ModelsTask>>(),
            _ => throw new ArgumentException($"Service not found for '{name}'")
        };

        return System.Threading.Tasks.Task.FromResult(service);
    }
}

