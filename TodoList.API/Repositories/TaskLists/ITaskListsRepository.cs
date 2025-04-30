using TodoList.API.Models;
using TodoList.API.Repositories.Requests;

namespace TodoList.API.Repositories.TaskLists;

public interface ITaskListsRepository
{
    Task CreateTaskListAsync(TaskListModel model, CancellationToken cancellationToken);
    Task ChangeTaskListAsync(Guid onBehalfOf, ChangeTaskListRequestModel request, CancellationToken cancellationToken);
    Task RemoveTaskListAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken);
    Task<(List<TaskListModel> data, bool hasNext)> GetManyTaskListsAsync(Guid onBehalfOf, int skip, int limit, CancellationToken cancellationToken);
    Task<bool> HasTaskListAccessAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken = default);
    Task<bool> HasTaskListOwnerAccessAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken = default);
    Task ShareTaskListAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task RestrictTaskListAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<List<UserModel>> GetTaskListUsersAsync(Guid id, CancellationToken cancellationToken);
}