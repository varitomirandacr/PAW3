using Microsoft.AspNetCore.Mvc;
using PAW3.Models.Entities;
using PAW3.ServiceLocator.Helper;

namespace PAW3.ServiceLocator.Controllers;

public class ServiceControllerBase : ControllerBase
{
    protected readonly Dictionary<string, Func<Task<IEnumerable<object>>>> ServiceResolvers;

    protected ServiceControllerBase(IServiceMapper serviceMapper)
    {
        ServiceResolvers = new()
        {
            ["product"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<Product>("product");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            },

            // Example for another service:
            // ["category"] = async () =>
            // {
            //     var service = await serviceMapper.GetServiceAsync<Category>("category");
            //     var data = await service.GetDataAsync();
            //     return data.Cast<object>();
            // }
        };
    }
}
