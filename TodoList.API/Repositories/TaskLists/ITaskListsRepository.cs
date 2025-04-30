using TodoList.API.Models;
using TodoList.API.Services.TaskLists.Contracts.Requests;

namespace TodoList.API.Repositories.TaskLists;

public interface ITaskListsRepository
{
    Task CreateTaskListAsync(TaskListModel model, CancellationToken cancellationToken);
    Task ChangeTaskListAsync(ChangeRequest request, CancellationToken cancellationToken);
    Task RemoveTaskListAsync(RemoveRequest request, CancellationToken cancellationToken);
    Task<(List<TaskListModel> data, bool hasNext)> GetManyTaskListsAsync(GetManyRequest request, CancellationToken cancellationToken);
    Task<bool> HasTaskListAccessAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken = default);
    Task<bool> HasTaskListOwnerAccessAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken = default);
    Task ShareTaskListAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task RestrictTaskListAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<List<UserModel>> GetTaskListUsersAsync(Guid id, CancellationToken cancellationToken);
}