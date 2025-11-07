using Microsoft.Extensions.Configuration;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.ServiceLocator.Extensions;

namespace PAW3.ServiceLocator.Services
{
    public class DogDataService : IDogDataService
    {
        private readonly IRestProvider _restProvider;
        private readonly IConfiguration _configuration;

        public DogDataService(IRestProvider restProvider, IConfiguration configuration)
        {
            _restProvider = restProvider;
            _configuration = configuration;
        }

        public async Task<string> GetDataAsync()
        {
            var url = _configuration.GetStringFromAppSettings("APIS", "Dogs");
            var response = await _restProvider.GetAsync(url, null);
            return JsonProvider.DeserializeSimple<string>(response) ?? string.Empty;
        }
    }

    public interface IDogDataService
    {
        Task<string> GetDataAsync();
    }
}
