using PAW3.Data.Foodbankdb.Models;
using PAW3.Data.Repositories;

namespace PAW3.Core.BusinessLogic;

public interface IFoodItemBusiness
{
    /// <summary>
    /// Deletes the fooditem associated with the fooditem id.
    /// </summary>
    /// <param name="id">The fooditem id.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteFoodItemAsync(int id);

    /// <summary>
    /// Gets food items. If id is provided, returns only that fooditem; otherwise returns all food items.
    /// </summary>
    /// <param name="id">Optional fooditem id.</param>
    /// <returns>A collection of food items.</returns>
    Task<IEnumerable<FoodItem>> GetFoodItems(int? id);

    /// <summary>
    /// Saves a fooditem (creates or updates).
    /// </summary>
    /// <param name="fooditem">The fooditem to save.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    Task<bool> SaveFoodItemAsync(FoodItem fooditem);
}

public class FooditemBusiness(IRepositoryFoodItem repositoryFoodItem) : IFoodItemBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveFoodItemAsync(FoodItem fooditem)
    {
        return await repositoryFoodItem.UpdateAsync(fooditem);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteFoodItemAsync(int id)
    {
        var fooditem = await repositoryFoodItem.FindAsync(id);
        if (fooditem == null) return false;
        return await repositoryFoodItem.DeleteAsync(fooditem);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FoodItem>> GetFoodItems(int? id)
    {
        return id == null
            ? await repositoryFoodItem.ReadAsync()
            : [await repositoryFoodItem.FindAsync((int)id)];
    }
}

