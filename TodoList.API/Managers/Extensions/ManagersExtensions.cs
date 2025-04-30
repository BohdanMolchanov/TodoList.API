using TodoList.API.Managers.TaskLists;

namespace TodoList.API.Managers.Extensions;

public static class ManagersExtensions
{
    public static void AddManagers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITaskListsManager, TaskListsManager>();
    }
}