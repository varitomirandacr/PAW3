using PAW3.Data.Repositories;
using PAW3.Models.Entities.Productdb;

namespace PAW3.Core.BusinessLogic;

public interface IUserActionBusiness
{
    /// <summary>
    /// Deletes the user action associated with the user action id.
    /// </summary>
    /// <param name="id">The user action id.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteUserActionAsync(int id);

    /// <summary>
    /// Gets user actions. If id is provided, returns only that user action; otherwise returns all user actions.
    /// </summary>
    /// <param name="id">Optional user action id.</param>
    /// <returns>A collection of user actions.</returns>
    Task<IEnumerable<UserAction>> GetUserActions(int? id);

    /// <summary>
    /// Saves a user action (creates or updates).
    /// </summary>
    /// <param name="userAction">The user action to save.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    Task<bool> SaveUserActionAsync(UserAction userAction);
}

public class UserActionBusiness(IRepositoryUserAction repositoryUserAction) : IUserActionBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveUserActionAsync(UserAction userAction)
    {
        return await repositoryUserAction.UpdateAsync(userAction);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteUserActionAsync(int id)
    {
        var userAction = await repositoryUserAction.FindAsync(id);
        if (userAction == null) return false;
        return await repositoryUserAction.DeleteAsync(userAction);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserAction>> GetUserActions(int? id)
    {
        return id == null
            ? await repositoryUserAction.ReadAsync()
            : [await repositoryUserAction.FindAsync((int)id)];
    }
}

