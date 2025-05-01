using TodoList.API.Models;
using TodoList.API.Models.ServiceResponse;
using TodoList.API.Services.TaskLists.Contracts.Requests;

namespace TodoList.API.Services.TaskLists;

public interface ITaskListsService
{
    Task<ServiceResponse<TaskListModel>> GetOneAsync(GetOneRequest request, CancellationToken cancellationToken);
    Task<ServiceResponse> CreateTaskListAsync(CreateRequest request, CancellationToken cancellationToken);
    Task<ServiceResponse> ChangeTaskListAsync(ChangeRequest request, CancellationToken cancellationToken);
    Task<ServiceResponse> RemoveTaskListAsync(RemoveRequest request, CancellationToken cancellationToken);
    Task<ServiceResponse<(List<TaskListModel> data, bool hasNext)>> GetManyTaskListsAsync(GetManyRequest request, CancellationToken cancellationToken);
    Task<ServiceResponse> ShareTaskListAsync(ChangeTaskListAccessRequestModel request, CancellationToken cancellationToken);
    Task<ServiceResponse> RestrictTaskListAsync(ChangeTaskListAccessRequestModel request, CancellationToken cancellationToken);
    Task<ServiceResponse<List<UserModel>>> GetTaskListUsersAsync(GetOneRequest request, CancellationToken cancellationToken);
}