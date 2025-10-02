namespace PAW3.ServiceLocator.Services.Contracts;

public interface IService<T>
{
    Task<IEnumerable<T>> GetDataAsync();
}
