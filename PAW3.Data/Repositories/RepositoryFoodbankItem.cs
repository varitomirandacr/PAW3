using PAW3.Data.Foodbankdb.Models;
using PAW3.Data.MSSQL;

namespace PAW3.Data.Repositories;

public interface IRepositoryFoodItem
{
    Task<bool> UpsertAsync(FoodItem entity, bool isUpdating);
    Task<bool> CreateAsync(FoodItem entity);
    Task<bool> DeleteAsync(FoodItem entity);
    Task<IEnumerable<FoodItem>> ReadAsync();
    Task<FoodItem> FindAsync(int id);
    Task<bool> UpdateAsync(FoodItem entity);
    Task<bool> UpdateManyAsync(IEnumerable<FoodItem> entities);
    Task<bool> ExistsAsync(FoodItem entity);
}

public class RepositoryFoodItem(FoodbankDbContext context) : FoodbankRepositoryBase<FoodItem>(context), IRepositoryFoodItem
{
}
