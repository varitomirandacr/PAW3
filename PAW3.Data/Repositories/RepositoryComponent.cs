using PAW3.Models.Entities;
using PAW3.Data.MSSQL;

namespace PAW3.Data.Repositories;

public interface IRepositoryComponent
{
    Task<bool> UpsertAsync(Component entity, bool isUpdating);
    Task<bool> CreateAsync(Component entity);
    Task<bool> DeleteAsync(Component entity);
    Task<IEnumerable<Component>> ReadAsync();
    Task<Component> FindAsync(int id);
    Task<bool> UpdateAsync(Component entity);
    Task<bool> UpdateManyAsync(IEnumerable<Component> entities);
    Task<bool> ExistsAsync(Component entity);
}

public class RepositoryComponent : RepositoryBase<Component>, IRepositoryComponent
{
    public RepositoryComponent(ProductDbContext context) : base(context)
    {
    }
}

