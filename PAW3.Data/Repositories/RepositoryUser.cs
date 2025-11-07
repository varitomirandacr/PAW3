using PAW3.Models.Entities;
using PAW3.Data.MSSQL;

namespace PAW3.Data.Repositories;

public interface IRepositoryUser
{
    Task<bool> UpsertAsync(User entity, bool isUpdating);
    Task<bool> CreateAsync(User entity);
    Task<bool> DeleteAsync(User entity);
    Task<IEnumerable<User>> ReadAsync();
    Task<User> FindAsync(int id);
    Task<bool> UpdateAsync(User entity);
    Task<bool> UpdateManyAsync(IEnumerable<User> entities);
    Task<bool> ExistsAsync(User entity);
}

public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
{
    public RepositoryUser(ProductDbContext context) : base(context)
    {
    }
}

