using PAW3.Data.Repositories;
using PAW3.Models.Entities.Productdb;

namespace PAW3.Core.BusinessLogic;

public interface ICategoryBusiness
{
    /// <summary>
    /// Deletes the category associated with the category id.
    /// </summary>
    /// <param name="id">The category id.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteCategoryAsync(int id);

    /// <summary>
    /// Gets categories. If id is provided, returns only that category; otherwise returns all categories.
    /// </summary>
    /// <param name="id">Optional category id.</param>
    /// <returns>A collection of categories.</returns>
    Task<IEnumerable<Category>> GetCategories(int? id);

    /// <summary>
    /// Saves a category (creates or updates).
    /// </summary>
    /// <param name="category">The category to save.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    Task<bool> SaveCategoryAsync(Category category);
}

public class CategoryBusiness(IRepositoryCategory repositoryCategory) : ICategoryBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveCategoryAsync(Category category)
    {
        return await repositoryCategory.UpdateAsync(category);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await repositoryCategory.FindAsync(id);
        if (category == null) return false;
        return await repositoryCategory.DeleteAsync(category);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Category>> GetCategories(int? id)
    {
        return id == null
            ? await repositoryCategory.ReadAsync()
            : [await repositoryCategory.FindAsync((int)id)];
    }
}

