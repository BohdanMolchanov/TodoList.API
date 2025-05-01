using TodoList.API.Managers.Basics;
using TodoList.API.Models;
using TodoList.API.Models.ServiceResponse;
using TodoList.API.Services.TaskLists;
using TodoList.API.Services.TaskLists.Contracts.Requests;
using TodoList.API.Services.Users;

namespace TodoList.API.Managers.TaskLists;

public class TaskListsManager(ITaskListsService service, IUsersService users) : AbstractManager(users), ITaskListsManager
{
    public async Task<ServiceResponse> CreateTaskListAsync(CreateRequest? request, CancellationToken cancellationToken)
    {
        if (request == null)
            return new ServiceResponse("payload", "required");
        
        var userResponse = await CheckManagingUserAsync(request.OnBehalfOf, cancellationToken);
        if (!userResponse.IsSuccess) return userResponse;

        return await service.CreateTaskListAsync(request, cancellationToken);
    }

    public async Task<ServiceResponse> ChangeTaskListAsync(ChangeRequest? request, CancellationToken cancellationToken)
    {
        if (request == null)
            return new ServiceResponse("payload", "required");
        
        var userResponse = await CheckManagingUserAsync(request.OnBehalfOf, cancellationToken);
        if (!userResponse.IsSuccess) return userResponse;

        return await service.ChangeTaskListAsync(request, cancellationToken);
    }

    public async Task<ServiceResponse> RemoveTaskListAsync(RemoveRequest? request, CancellationToken cancellationToken)
    {
        if (request == null)
            return new ServiceResponse("payload", "required");
        
        var userResponse = await CheckManagingUserAsync(request.OnBehalfOf, cancellationToken);
        if (!userResponse.IsSuccess) return userResponse;

        return await service.RemoveTaskListAsync(request, cancellationToken);
    }

    public async Task<ServiceResponse<(List<TaskListModel> data, bool hasNext)>> GetManyTaskListsAsync(GetManyRequest? request, CancellationToken cancellationToken)
    {
        if (request == null)
            return new ServiceResponse<(List<TaskListModel> data, bool hasNext)>("payload", "required");
        
        var userResponse = await CheckManagingUserAsync<(List<TaskListModel> data, bool hasNext)>(request.OnBehalfOf, cancellationToken);
        if (!userResponse.IsSuccess) return userResponse;

        return await service.GetManyTaskListsAsync(request, cancellationToken);
    }

    public async Task<ServiceResponse> ShareTaskListAsync(ChangeTaskListAccessRequestModel? request, CancellationToken cancellationToken)
    {
        if (request == null)
            return new ServiceResponse("payload", "required");
        
        var userResponse = await CheckManagingUserAsync(request.OnBehalfOf, cancellationToken);
        if (!userResponse.IsSuccess) return userResponse;
        
        var linkUserResponse = await CheckManagingUserAsync(request.UserId, cancellationToken);
        if (!linkUserResponse.IsSuccess) return linkUserResponse;

        return await service.ShareTaskListAsync(request, cancellationToken);
    }

    public async Task<ServiceResponse> RestrictTaskListAsync(ChangeTaskListAccessRequestModel? request, CancellationToken cancellationToken)
    {
        if (request == null)
            return new ServiceResponse("payload", "required");
        
        var userResponse = await CheckManagingUserAsync(request.OnBehalfOf, cancellationToken);
        if (!userResponse.IsSuccess) return userResponse;
        
        var linkUserResponse = await CheckManagingUserAsync(request.UserId, cancellationToken);
        if (!linkUserResponse.IsSuccess) return linkUserResponse;

        return await service.RestrictTaskListAsync(request, cancellationToken);
    }

    public async Task<ServiceResponse<List<UserModel>>> GetTaskListUsersAsync(GetOneRequest? request, CancellationToken cancellationToken)
    {
        if (request == null)
            return new ServiceResponse<List<UserModel>>("payload", "required");
        
        var userResponse = await CheckManagingUserAsync<List<UserModel>>(request.OnBehalfOf, cancellationToken);
        if (!userResponse.IsSuccess) return userResponse;

        return await service.GetTaskListUsersAsync(request, cancellationToken);
    }
}