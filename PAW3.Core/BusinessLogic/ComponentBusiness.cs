using PAW3.Data.Repositories;
using PAW3.Models.Entities.Productdb;

namespace PAW3.Core.BusinessLogic;

public interface IComponentBusiness
{
    /// <summary>
    /// Deletes the component associated with the component id.
    /// </summary>
    /// <param name="id">The component id.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteComponentAsync(int id);

    /// <summary>
    /// Gets components. If id is provided, returns only that component; otherwise returns all components.
    /// </summary>
    /// <param name="id">Optional component id.</param>
    /// <returns>A collection of components.</returns>
    Task<IEnumerable<Component>> GetComponents(int? id);

    /// <summary>
    /// Saves a component (creates or updates).
    /// </summary>
    /// <param name="component">The component to save.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    Task<bool> SaveComponentAsync(Component component);
}

public class ComponentBusiness(IRepositoryComponent repositoryComponent) : IComponentBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveComponentAsync(Component component)
    {
        return await repositoryComponent.UpdateAsync(component);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteComponentAsync(int id)
    {
        var component = await repositoryComponent.FindAsync(id);
        if (component == null) return false;
        return await repositoryComponent.DeleteAsync(component);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Component>> GetComponents(int? id)
    {
        return id == null
            ? await repositoryComponent.ReadAsync()
            : [await repositoryComponent.FindAsync((int)id)];
    }
}

