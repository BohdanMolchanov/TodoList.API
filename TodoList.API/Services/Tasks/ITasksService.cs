using TodoList.API.Models;
using TodoList.API.Models.ServiceResponse;

namespace TodoList.API.Services.Tasks;

public interface ITasksService
{
    Task<ServiceResponse<List<TaskModel>>> GetManyAsync(Guid taskListId, Guid onBehalfOf, CancellationToken cancellationToken);
    Task<ServiceResponse> CompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken);
    Task<ServiceResponse> InCompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken);
}