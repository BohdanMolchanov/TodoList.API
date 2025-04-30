using TodoList.API.Models;
using TodoList.API.Models.ServiceResponse;
using TodoList.API.Repositories.TaskLists;
using TodoList.API.Services.TaskLists.Contracts.Requests;

namespace TodoList.API.Services.TaskLists;

public class TaskListsService(ITaskListsRepository repository) : ITaskListsService
{
    public async Task<ServiceResponse> CreateTaskListAsync(CreateRequest request, CancellationToken cancellationToken)
    {
        var validation = new CreateRequestValidator().Validate(request);
        if (!validation.IsValid)
            return new ServiceResponse(validation);
        
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

    public async Task<ServiceResponse> ChangeTaskListAsync(ChangeRequest request, CancellationToken cancellationToken)
    {
        var validation = new ChangeRequestValidator().Validate(request);
        if (!validation.IsValid)
            return new ServiceResponse(validation);
        
        var accessResponse = await CheckTaskListAccessAsync(request.TaskListId!.Value, request.OnBehalfOf!.Value, cancellationToken);
        if (!accessResponse.IsSuccess)
            return new ServiceResponse()
            {
                Errors = accessResponse.Errors
            };

        await repository.ChangeTaskListAsync(request, cancellationToken);
        
        return new ServiceResponse();
    }

    public async Task<ServiceResponse> RemoveTaskListAsync(RemoveRequest request, CancellationToken cancellationToken)
    {
        var validation = new RemoveRequestValidator().Validate(request);
        if (!validation.IsValid)
            return new ServiceResponse(validation);
        
        var accessResponse = await CheckTaskListAccessAsync(request.TaskListId!.Value, request.OnBehalfOf!.Value, cancellationToken);
        if (!accessResponse.IsSuccess)
            return new ServiceResponse()
            {
                Errors = accessResponse.Errors
            };
        
        await repository.RemoveTaskListAsync(request, cancellationToken);
        
        return new ServiceResponse();
    }

    public async Task<ServiceResponse<(List<TaskListModel> data, bool hasNext)>> GetManyTaskListsAsync(
        GetManyRequest request, CancellationToken cancellationToken)
    {
        var validation = new GetManyRequestValidator().Validate(request);
        if (!validation.IsValid)
            return new ServiceResponse<(List<TaskListModel> data, bool hasNext)>(validation);
        
        var result = await repository.GetManyTaskListsAsync(request, cancellationToken);
        return new ServiceResponse<(List<TaskListModel> data, bool hasNext)>()
        {
            Result = result
        };
    }

    public async Task<ServiceResponse> ShareTaskListAsync(ChangeTaskListAccessRequestModel request, CancellationToken cancellationToken)
    {
        var validation = new ChangeTaskListAccessRequestModelValidator().Validate(request);
        if (!validation.IsValid)
            return new ServiceResponse(validation);
        
        var accessResponse = await CheckTaskListAccessAsync(request.Id, request.OnBehalfOf!.Value, cancellationToken);
        if (!accessResponse.IsSuccess)
            return new ServiceResponse()
            {
                Errors = accessResponse.Errors
            };

        await repository.ShareTaskListAsync(request.Id, request.UserId!.Value, cancellationToken);
        
        return new ServiceResponse();
    }

    public async Task<ServiceResponse> RestrictTaskListAsync(ChangeTaskListAccessRequestModel request, CancellationToken cancellationToken)
    {
        var validation = new ChangeTaskListAccessRequestModelValidator().Validate(request);
        if (!validation.IsValid)
            return new ServiceResponse(validation);
        
        var accessResponse = await CheckTaskListAccessAsync(request.Id, request.OnBehalfOf!.Value, cancellationToken);
        if (!accessResponse.IsSuccess)
            return new ServiceResponse()
            {
                Errors = accessResponse.Errors
            };

        await repository.RestrictTaskListAsync(request.Id, request.UserId!.Value, cancellationToken);
        
        return new ServiceResponse();
    }

    public async Task<ServiceResponse<List<UserModel>>> GetTaskListUsersAsync(GetOneRequest request, CancellationToken cancellationToken)
    {
        var validation = new GetOneRequestValidator().Validate(request);
        if (!validation.IsValid)
            return new ServiceResponse<List<UserModel>>(validation);
        
        var accessResponse = await CheckTaskListAccessAsync(request.TaskListId!.Value, request.OnBehalfOf!.Value, cancellationToken);
        if (!accessResponse.IsSuccess)
            return new ServiceResponse<List<UserModel>>()
            {
                Errors = accessResponse.Errors
            };
        
        var result = await repository.GetTaskListUsersAsync(request.OnBehalfOf!.Value, cancellationToken);
        return new ServiceResponse<List<UserModel>>()
        {
            Result = result
        };
    }

    private async Task<ServiceResponse> CheckTaskListAccessAsync(Guid id, Guid onBehalfOf,
        CancellationToken cancellationToken = default)
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
        return new ServiceResponse();
    }
}