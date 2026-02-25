using PAW3.Data.MSSQL;
using ModelsTask = PAW3.Models.Entities.Productdb.Task;

namespace PAW3.Data.Repositories;

public interface IRepositoryTask
{
    System.Threading.Tasks.Task<bool> UpsertAsync(ModelsTask entity, bool isUpdating);
    System.Threading.Tasks.Task<bool> CreateAsync(ModelsTask entity);
    System.Threading.Tasks.Task<bool> DeleteAsync(ModelsTask entity);
    System.Threading.Tasks.Task<IEnumerable<ModelsTask>> ReadAsync();
    System.Threading.Tasks.Task<ModelsTask> FindAsync(int id);
    System.Threading.Tasks.Task<bool> UpdateAsync(ModelsTask entity);
    System.Threading.Tasks.Task<bool> UpdateManyAsync(IEnumerable<ModelsTask> entities);
    System.Threading.Tasks.Task<bool> ExistsAsync(ModelsTask entity);
}

public class RepositoryTask : RepositoryBase<ModelsTask>, IRepositoryTask
{
    public RepositoryTask(ProductDbContext context) : base(context)
    {
    }
}

