using TodoList.API.Models;
using TodoList.API.Models.ServiceResponse;
using TodoList.API.Services.Requests;

namespace TodoList.API.Services.Users;

public interface IUsersService
{
    Task<ServiceResponse<UserModel>> CreateUserAsync(CreateUserRequestModel request, CancellationToken cancellationToken);
    Task<ServiceResponse<List<UserModel>>> GetUsersAsync(CancellationToken cancellationToken);
}