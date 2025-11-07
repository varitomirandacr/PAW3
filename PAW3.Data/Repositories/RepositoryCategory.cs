using PAW3.Models.Entities;
using PAW3.Data.MSSQL;

namespace PAW3.Data.Repositories;

public interface IRepositoryCategory
{
    Task<bool> UpsertAsync(Category entity, bool isUpdating);
    Task<bool> CreateAsync(Category entity);
    Task<bool> DeleteAsync(Category entity);
    Task<IEnumerable<Category>> ReadAsync();
    Task<Category> FindAsync(int id);
    Task<bool> UpdateAsync(Category entity);
    Task<bool> UpdateManyAsync(IEnumerable<Category> entities);
    Task<bool> ExistsAsync(Category entity);
}

public class RepositoryCategory : RepositoryBase<Category>, IRepositoryCategory
{
    public RepositoryCategory(ProductDbContext context) : base(context)
    {
    }
}

