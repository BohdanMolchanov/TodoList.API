using TodoList.API.Models;

namespace TodoList.API.Repositories.Users;

public interface IUsersRepository
{
    Task CreateUserAsync(UserModel model, CancellationToken cancellationToken);
    Task<List<UserModel>> GetUsersAsync(CancellationToken cancellationToken);
    Task<UserModel?> GetOneAsync(Guid onBehalfOf, CancellationToken cancellationToken);
}