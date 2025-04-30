using TodoList.API.Models.ServiceResponse;
using TodoList.API.Services.Users;

namespace TodoList.API.Managers.Basics;

public class AbstractManager(IUsersService users)
{
    protected async Task<ServiceResponse> CheckManagingUserAsync(Guid? onBehalfOf, CancellationToken cancellationToken)
    {
        if (!onBehalfOf.HasValue)
            return new ServiceResponse(nameof(onBehalfOf), "required");

        return await users.GetOneAsync(onBehalfOf.Value, cancellationToken);
    }
    
    protected async Task<ServiceResponse<T>> CheckManagingUserAsync<T>(Guid? onBehalfOf, CancellationToken cancellationToken)
    {
        if (!onBehalfOf.HasValue)
            return new ServiceResponse<T>(nameof(onBehalfOf), "required");

        var response = await users.GetOneAsync(onBehalfOf.Value, cancellationToken);
        return new ServiceResponse<T>()
        {
            Errors = response.Errors
        };
    }
}