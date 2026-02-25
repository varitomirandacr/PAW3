using PAW3.Data.MSSQL;
using PAW3.Models.Entities.Productdb;

namespace PAW3.Data.Repositories;

public interface IRepositoryNotification
{
    Task<bool> UpsertAsync(Notification entity, bool isUpdating);
    Task<bool> CreateAsync(Notification entity);
    Task<bool> DeleteAsync(Notification entity);
    Task<IEnumerable<Notification>> ReadAsync();
    Task<Notification> FindAsync(int id);
    Task<bool> UpdateAsync(Notification entity);
    Task<bool> UpdateManyAsync(IEnumerable<Notification> entities);
    Task<bool> ExistsAsync(Notification entity);
}

public class RepositoryNotification : RepositoryBase<Notification>, IRepositoryNotification
{
    public RepositoryNotification(ProductDbContext context) : base(context)
    {
    }
}

