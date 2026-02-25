using PAW3.Data.Repositories;
using PAW3.Models.Entities.Productdb;

namespace PAW3.Core.BusinessLogic;

public interface IUserRoleBusiness
{
    /// <summary>
    /// Deletes the user role associated with the user role id.
    /// </summary>
    /// <param name="id">The user role id.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteUserRoleAsync(int id);

    /// <summary>
    /// Gets user roles. If id is provided, returns only that user role; otherwise returns all user roles.
    /// </summary>
    /// <param name="id">Optional user role id.</param>
    /// <returns>A collection of user roles.</returns>
    Task<IEnumerable<UserRole>> GetUserRoles(int? id);

    /// <summary>
    /// Saves a user role (creates or updates).
    /// </summary>
    /// <param name="userRole">The user role to save.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    Task<bool> SaveUserRoleAsync(UserRole userRole);
}

public class UserRoleBusiness(IRepositoryUserRole repositoryUserRole) : IUserRoleBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveUserRoleAsync(UserRole userRole)
    {
        return await repositoryUserRole.UpdateAsync(userRole);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteUserRoleAsync(int id)
    {
        var userRole = await repositoryUserRole.FindAsync(id);
        if (userRole == null) return false;
        return await repositoryUserRole.DeleteAsync(userRole);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserRole>> GetUserRoles(int? id)
    {
        return id == null
            ? await repositoryUserRole.ReadAsync()
            : [await repositoryUserRole.FindAsync((int)id)];
    }
}

