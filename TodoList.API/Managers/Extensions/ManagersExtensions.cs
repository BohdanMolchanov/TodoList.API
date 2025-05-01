using TodoList.API.Managers.TaskLists;
using TodoList.API.Managers.Tasks;

namespace TodoList.API.Managers.Extensions;

public static class ManagersExtensions
{
    public static void AddManagers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITaskListsManager, TaskListsManager>();
        serviceCollection.AddSingleton<ITasksManager, TasksManager>();
    }
}