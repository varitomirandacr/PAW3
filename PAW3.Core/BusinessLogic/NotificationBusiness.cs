using PAW3.Data.Repositories;
using PAW3.Models.Entities.Productdb;

namespace PAW3.Core.BusinessLogic;

public interface INotificationBusiness
{
    /// <summary>
    /// Deletes the notification associated with the notification id.
    /// </summary>
    /// <param name="id">The notification id.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteNotificationAsync(int id);

    /// <summary>
    /// Gets notifications. If id is provided, returns only that notification; otherwise returns all notifications.
    /// </summary>
    /// <param name="id">Optional notification id.</param>
    /// <returns>A collection of notifications.</returns>
    Task<IEnumerable<Notification>> GetNotifications(int? id);

    /// <summary>
    /// Saves a notification (creates or updates).
    /// </summary>
    /// <param name="notification">The notification to save.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    Task<bool> SaveNotificationAsync(Notification notification);
}

public class NotificationBusiness(IRepositoryNotification repositoryNotification) : INotificationBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveNotificationAsync(Notification notification)
    {
        return await repositoryNotification.UpdateAsync(notification);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteNotificationAsync(int id)
    {
        var notification = await repositoryNotification.FindAsync(id);
        if (notification == null) return false;
        return await repositoryNotification.DeleteAsync(notification);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Notification>> GetNotifications(int? id)
    {
        return id == null
            ? await repositoryNotification.ReadAsync()
            : [await repositoryNotification.FindAsync((int)id)];
    }
}

