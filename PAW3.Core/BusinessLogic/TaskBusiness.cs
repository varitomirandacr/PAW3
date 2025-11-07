using PAW3.Models.Entities;
using PAW3.Data.Repositories;
using ModelsTask = PAW3.Models.Entities.Task;

namespace PAW3.Core.BusinessLogic;

public interface ITaskBusiness
{
    /// <summary>
    /// Deletes the task associated with the task id.
    /// </summary>
    /// <param name="id">The task id.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteTaskAsync(int id);

    /// <summary>
    /// Gets tasks. If id is provided, returns only that task; otherwise returns all tasks.
    /// </summary>
    /// <param name="id">Optional task id.</param>
    /// <returns>A collection of tasks.</returns>
    Task<IEnumerable<ModelsTask>> GetTasks(int? id);

    /// <summary>
    /// Saves a task (creates or updates).
    /// </summary>
    /// <param name="task">The task to save.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    Task<bool> SaveTaskAsync(ModelsTask task);
}

public class TaskBusiness(IRepositoryTask repositoryTask) : ITaskBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveTaskAsync(ModelsTask task)
    {
        return await repositoryTask.UpdateAsync(task);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await repositoryTask.FindAsync(id);
        if (task == null) return false;
        return await repositoryTask.DeleteAsync(task);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ModelsTask>> GetTasks(int? id)
    {
        return id == null
            ? await repositoryTask.ReadAsync()
            : [await repositoryTask.FindAsync((int)id)];
    }
}

