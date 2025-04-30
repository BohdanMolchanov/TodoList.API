using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TodoList.API.Data;
using TodoList.API.Repositories.TaskLists;
using TodoList.API.Repositories.Tasks;
using TodoList.API.Repositories.Users;

namespace TodoList.API.Repositories.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        serviceCollection.AddDbContextFactory<TodoListContext>(d =>
            d.UseNpgsql(configuration.GetConnectionString("todoDb")));
        
        serviceCollection.TryAddSingleton<IUsersRepository, UsersRepository>();
        serviceCollection.TryAddSingleton<ITaskListsRepository, TaskListsRepository>();
        serviceCollection.TryAddSingleton<ITasksRepository, TasksRepository>();
        
        return serviceCollection;
    }
}