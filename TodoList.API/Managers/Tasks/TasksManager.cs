using TodoList.API.Managers.Basics;
using TodoList.API.Models;
using TodoList.API.Models.ServiceResponse;
using TodoList.API.Services.Tasks;
using TodoList.API.Services.Tasks.Contracts;
using TodoList.API.Services.Users;

namespace TodoList.API.Managers.Tasks;

public class TasksManager(ITasksService service, IUsersService usersService) 
    : AbstractManager(usersService), ITasksManager
{
    public async Task<ServiceResponse<List<TaskModel>>> GetManyAsync(Guid taskListId, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        var userResponse = await CheckManagingUserAsync<List<TaskModel>>(onBehalfOf, cancellationToken);
        if (!userResponse.IsSuccess) return userResponse;

        return await service.GetManyAsync(taskListId, onBehalfOf, cancellationToken);
    }

    public async Task<ServiceResponse> CompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        var userResponse = await CheckManagingUserAsync(onBehalfOf, cancellationToken);
        if (!userResponse.IsSuccess) return userResponse;

        return await service.CompleteTaskAsync(taskId, onBehalfOf, cancellationToken);
    }

    public async Task<ServiceResponse> InCompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        var userResponse = await CheckManagingUserAsync(onBehalfOf, cancellationToken);
        if (!userResponse.IsSuccess) return userResponse;

        return await service.InCompleteTaskAsync(taskId, onBehalfOf, cancellationToken);
    }

    public async Task<ServiceResponse> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var userResponse = await CheckManagingUserAsync(request.OnBehalfOf, cancellationToken);
        if (!userResponse.IsSuccess) return userResponse;

        return await service.CreateAsync(request, cancellationToken);
    }
}