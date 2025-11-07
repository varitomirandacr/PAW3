using PAW3.Models.Entities;
using PAW3.Data.MSSQL;

namespace PAW3.Data.Repositories;

public interface IRepositoryInventory
{
    Task<bool> UpsertAsync(Inventory entity, bool isUpdating);
    Task<bool> CreateAsync(Inventory entity);
    Task<bool> DeleteAsync(Inventory entity);
    Task<IEnumerable<Inventory>> ReadAsync();
    Task<Inventory> FindAsync(int id);
    Task<bool> UpdateAsync(Inventory entity);
    Task<bool> UpdateManyAsync(IEnumerable<Inventory> entities);
    Task<bool> ExistsAsync(Inventory entity);
}

public class ReposityryInventory : RepositoryBase<Inventory>, IRepositoryInventory
{
    public ReposityryInventory(ProductDbContext context) : base(context)
    {
    }
}

