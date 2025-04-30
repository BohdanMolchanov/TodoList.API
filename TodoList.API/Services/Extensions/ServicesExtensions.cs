using Microsoft.Extensions.DependencyInjection.Extensions;
using TodoList.API.Services.TaskLists;
using TodoList.API.Services.Tasks;
using TodoList.API.Services.Users;

namespace TodoList.API.Services.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddSingleton<IUsersService, UsersService>();
        serviceCollection.TryAddSingleton<ITaskListsService, TaskListsService>();
        serviceCollection.TryAddSingleton<ITasksService, TasksService>();
        
        return serviceCollection;
    }
}