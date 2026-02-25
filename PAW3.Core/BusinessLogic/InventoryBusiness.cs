using PAW3.Data.Repositories;
using PAW3.Models.Entities.Productdb;

namespace PAW3.Core.BusinessLogic;

public interface IInventoryBusiness
{
    /// <summary>
    /// Deletes the inventory associated with the inventory id.
    /// </summary>
    /// <param name="id">The inventory id.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteInventoryAsync(int id);

    /// <summary>
    /// Gets inventories. If id is provided, returns only that inventory; otherwise returns all inventories.
    /// </summary>
    /// <param name="id">Optional inventory id.</param>
    /// <returns>A collection of inventories.</returns>
    Task<IEnumerable<Inventory>> GetInventories(int? id);

    /// <summary>
    /// Saves an inventory (creates or updates).
    /// </summary>
    /// <param name="inventory">The inventory to save.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    Task<bool> SaveInventoryAsync(Inventory inventory);
}

public class InventoryBusiness(IRepositoryInventory repositoryInventory) : IInventoryBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveInventoryAsync(Inventory inventory)
    {
        return await repositoryInventory.UpdateAsync(inventory);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteInventoryAsync(int id)
    {
        var inventory = await repositoryInventory.FindAsync(id);
        if (inventory == null) return false;
        return await repositoryInventory.DeleteAsync(inventory);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Inventory>> GetInventories(int? id)
    {
        return id == null
            ? await repositoryInventory.ReadAsync()
            : [await repositoryInventory.FindAsync((int)id)];
    }
}

