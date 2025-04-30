using TodoList.API.Models;
using TodoList.API.Models.ServiceResponse;
using TodoList.API.Repositories.Requests;
using TodoList.API.Repositories.TaskLists;
using TodoList.API.Services.Requests;

namespace TodoList.API.Services.TaskLists;

public class TaskListsService(ITaskListsRepository repository) : ITaskListsService
{
    public async Task<ServiceResponse> CreateTaskListAsync(CreateTaskListRequestModel request, CancellationToken cancellationToken)
    {
        var model = new TaskListModel()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            OwnerId = request.OnBehalfOf
        };
        
        await repository.CreateTaskListAsync(model, cancellationToken);
        
        return new ServiceResponse();
    }

    public async Task<ServiceResponse> ChangeTaskListAsync(Guid onBehalfOf, ChangeTaskListRequestModel request, CancellationToken cancellationToken)
    {
        var hasAccess = await repository.HasTaskListAccessAsync(request.TaskListId, onBehalfOf, cancellationToken);
        if(!hasAccess)
            return new ServiceResponse()
            {
                Errors =
                [
                    new ErrorModel()
                    {
                        Message = "You do not have permission to this taskList.",
                        Property = "taskList"
                    }
                ]
            };

        await repository.ChangeTaskListAsync(onBehalfOf, request, cancellationToken);
        
        return new ServiceResponse();
    }

    public async Task<ServiceResponse> RemoveTaskListAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        var hasAccess = await repository.HasTaskListOwnerAccessAsync(id, onBehalfOf, cancellationToken);
        if(!hasAccess)
            return new ServiceResponse()
            {
                Errors =
                [
                    new ErrorModel()
                    {
                        Message = "You do not have permission to remove this taskList.",
                        Property = "taskList"
                    }
                ]
            };
        
        await repository.RemoveTaskListAsync(onBehalfOf, onBehalfOf, cancellationToken);
        
        return new ServiceResponse();
    }

    public async Task<ServiceResponse<(List<TaskListModel> data, bool hasNext)>> GetManyTaskListsAsync(Guid onBehalfOf, int skip, int limit, CancellationToken cancellationToken)
    {
        var result = await repository.GetManyTaskListsAsync(onBehalfOf, skip, limit, cancellationToken);
        return new ServiceResponse<(List<TaskListModel> data, bool hasNext)>()
        {
            Result = result
        };
    }

    public async Task<ServiceResponse> ShareTaskListAsync(ChangeTaskListAccessRequestModel request, CancellationToken cancellationToken)
    {
        var hasAccess = await repository.HasTaskListAccessAsync(request.Id, request.OnBehalfOf, cancellationToken);
        if(!hasAccess)
            return new ServiceResponse()
            {
                Errors =
                [
                    new ErrorModel()
                    {
                        Message = "You do not have permission to this taskList.",
                        Property = "taskList"
                    }
                ]
            };

        await repository.ShareTaskListAsync(request.Id, request.UserId, cancellationToken);
        
        return new ServiceResponse();
    }

    public async Task<ServiceResponse> RestrictTaskListAsync(ChangeTaskListAccessRequestModel request, CancellationToken cancellationToken)
    {
        var hasAccess = await repository.HasTaskListAccessAsync(request.Id, request.OnBehalfOf, cancellationToken);
        if(!hasAccess)
            return new ServiceResponse()
            {
                Errors =
                [
                    new ErrorModel()
                    {
                        Message = "You do not have permission to this taskList.",
                        Property = "taskList"
                    }
                ]
            };

        await repository.RestrictTaskListAsync(request.Id, request.UserId, cancellationToken);
        
        return new ServiceResponse();
    }

    public async Task<ServiceResponse<List<UserModel>>> GetTaskListUsersAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        var hasAccess = await repository.HasTaskListAccessAsync(id, onBehalfOf, cancellationToken);
        if(!hasAccess)
            return new ServiceResponse<List<UserModel>>()
            {
                Errors =
                [
                    new ErrorModel()
                    {
                        Message = "You do not have permission to this taskList.",
                        Property = "taskList"
                    }
                ]
            };
        
        var result = await repository.GetTaskListUsersAsync(onBehalfOf, cancellationToken);
        return new ServiceResponse<List<UserModel>>()
        {
            Result = result
        };
    }
}