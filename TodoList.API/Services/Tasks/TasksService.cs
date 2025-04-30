using TodoList.API.Models;
using TodoList.API.Models.ServiceResponse;
using TodoList.API.Repositories.Tasks;

namespace TodoList.API.Services.Tasks;

public class TasksService(ITasksRepository repository) : ITasksService
{
    public async Task<ServiceResponse<List<TaskModel>>> GetManyAsync(Guid taskListId, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        var models = await repository.GetManyAsync(taskListId, onBehalfOf, cancellationToken);
        return new ServiceResponse<List<TaskModel>>()
        {
            Result = models
        };
    }

    public async Task<ServiceResponse> CompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        var isUpdated = await repository.CompleteTaskAsync(taskId, onBehalfOf, cancellationToken);
        
        if (!isUpdated)
            return new ServiceResponse()
            {
                Errors =
                [
                    new ErrorModel()
                    {
                        Message = "You do not have permission to this task.",
                        Property = "task"
                    }
                ]
            };
        
        return new ServiceResponse();
    }

    public async Task<ServiceResponse> InCompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        var isUpdated = await repository.InCompleteTaskAsync(taskId, onBehalfOf, cancellationToken);
        
        if (!isUpdated)
            return new ServiceResponse()
            {
                Errors =
                [
                    new ErrorModel()
                    {
                        Message = "You do not have permission to this task.",
                        Property = "task"
                    }
                ]
            };
        
        return new ServiceResponse();
    }
}