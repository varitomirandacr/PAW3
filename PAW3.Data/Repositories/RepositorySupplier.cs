using PAW3.Data.MSSQL;
using PAW3.Models.Entities.Productdb;

namespace PAW3.Data.Repositories;

public interface IRepositorySupplier
{
    Task<bool> UpsertAsync(Supplier entity, bool isUpdating);
    Task<bool> CreateAsync(Supplier entity);
    Task<bool> DeleteAsync(Supplier entity);
    Task<IEnumerable<Supplier>> ReadAsync();
    Task<Supplier> FindAsync(int id);
    Task<bool> UpdateAsync(Supplier entity);
    Task<bool> UpdateManyAsync(IEnumerable<Supplier> entities);
    Task<bool> ExistsAsync(Supplier entity);
}

public class RepositorySupplier : RepositoryBase<Supplier>, IRepositorySupplier
{
    public RepositorySupplier(ProductDbContext context) : base(context)
    {
    }
}

