using TodoList.API.Models;
using TodoList.API.Models.ServiceResponse;
using TodoList.API.Repositories.Users;
using TodoList.API.Services.Requests;

namespace TodoList.API.Services.Users;

public class UsersService(IUsersRepository repository) : IUsersService
{
    public async Task<ServiceResponse<UserModel>> CreateUserAsync(CreateUserRequestModel request, CancellationToken cancellationToken)
    {
        var model = new UserModel()
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            CreatedAt = DateTime.UtcNow
        };
        
        await repository.CreateUserAsync(model, cancellationToken);

        return new ServiceResponse<UserModel>()
        {
            Result = model
        };
    }

    public async Task<ServiceResponse<List<UserModel>>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var result = await repository.GetUsersAsync(cancellationToken);
        return new ServiceResponse<List<UserModel>>()
        {
            Result = result
        };
    }
}