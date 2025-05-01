using TodoList.API.Models;

namespace TodoList.API.Repositories.Tasks;

public interface ITasksRepository
{
    Task<List<TaskModel>> GetManyAsync(Guid taskListId, Guid onBehalfOf, CancellationToken cancellationToken);
    Task<bool> CompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken);
    Task<bool> InCompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken);
    Task CreateTaskAsync(TaskModel model, CancellationToken cancellationToken);
}