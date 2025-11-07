using PAW3.Models.Entities;
using PAW3.Data.Repositories;

namespace PAW3.Core.BusinessLogic;

public interface IRoleBusiness
{
    /// <summary>
    /// Deletes the role associated with the role id.
    /// </summary>
    /// <param name="id">The role id.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteRoleAsync(int id);

    /// <summary>
    /// Gets roles. If id is provided, returns only that role; otherwise returns all roles.
    /// </summary>
    /// <param name="id">Optional role id.</param>
    /// <returns>A collection of roles.</returns>
    Task<IEnumerable<Role>> GetRoles(int? id);

    /// <summary>
    /// Saves a role (creates or updates).
    /// </summary>
    /// <param name="role">The role to save.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    Task<bool> SaveRoleAsync(Role role);
}

public class RoleBusiness(IRepositoryRole repositoryRole) : IRoleBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveRoleAsync(Role role)
    {
        return await repositoryRole.UpdateAsync(role);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteRoleAsync(int id)
    {
        var role = await repositoryRole.FindAsync(id);
        if (role == null) return false;
        return await repositoryRole.DeleteAsync(role);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Role>> GetRoles(int? id)
    {
        return id == null
            ? await repositoryRole.ReadAsync()
            : [await repositoryRole.FindAsync((int)id)];
    }
}

