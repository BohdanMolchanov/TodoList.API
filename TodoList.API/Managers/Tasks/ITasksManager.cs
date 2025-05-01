using TodoList.API.Models;
using TodoList.API.Models.ServiceResponse;
using TodoList.API.Services.Tasks.Contracts;

namespace TodoList.API.Managers.Tasks;

public interface ITasksManager
{
    Task<ServiceResponse<List<TaskModel>>> GetManyAsync(Guid taskListId, Guid onBehalfOf, CancellationToken cancellationToken);
    Task<ServiceResponse> CompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken);
    Task<ServiceResponse> InCompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken);
    Task<ServiceResponse> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken);
}