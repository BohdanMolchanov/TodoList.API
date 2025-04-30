using TodoList.API.Models;
using TodoList.API.Models.ServiceResponse;
using TodoList.API.Repositories.Requests;
using TodoList.API.Services.Requests;

namespace TodoList.API.Services.TaskLists;

public interface ITaskListsService
{
    Task<ServiceResponse> CreateTaskListAsync(CreateTaskListRequestModel request, CancellationToken cancellationToken);
    Task<ServiceResponse> ChangeTaskListAsync(Guid onBehalfOf, ChangeTaskListRequestModel request, CancellationToken cancellationToken);
    Task<ServiceResponse> RemoveTaskListAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken);
    Task<ServiceResponse<(List<TaskListModel> data, bool hasNext)>> GetManyTaskListsAsync(Guid onBehalfOf, int skip, int limit, CancellationToken cancellationToken);
    Task<ServiceResponse> ShareTaskListAsync(ChangeTaskListAccessRequestModel request, CancellationToken cancellationToken);
    Task<ServiceResponse> RestrictTaskListAsync(ChangeTaskListAccessRequestModel request, CancellationToken cancellationToken);
    Task<ServiceResponse<List<UserModel>>> GetTaskListUsersAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken);
}