using Microsoft.EntityFrameworkCore;
using TodoList.API.Data;
using TodoList.API.Extensions;
using TodoList.API.Models;

namespace TodoList.API.Repositories.Tasks;

public class TasksRepository(DbContextOptions<TodoListContext> contextOptions) : ITasksRepository
{
    public async Task<List<TaskModel>> GetManyAsync(Guid taskListId, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        await using var context = new TodoListContext(contextOptions);
        var entities = await context.Tasks.AsNoTracking()
            .Where(x => 
                x.TaskListId == taskListId &&
                (x.TaskList.OwnerId == onBehalfOf ||
                        x.TaskList.UserLinks.Any(u => u.UserId == onBehalfOf)))
            .OrderByDescending(x => x.IsCompleted)
            .ToListAsync(cancellationToken: cancellationToken);

        return entities.Select(x => x.ToModel()).ToList();
    }

    public async Task<bool> CompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        await using var context = new TodoListContext(contextOptions);
        var result = await context.Tasks.AsNoTracking()
            .Where(x => 
                x.Id == taskId &&
                (x.TaskList.OwnerId == onBehalfOf ||
                 x.TaskList.UserLinks.Any(u => u.UserId == onBehalfOf)))
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.IsCompleted, true)
                    .SetProperty(p => p.LastUpdatedAt, DateTime.UtcNow), 
                cancellationToken: cancellationToken);
        
        return result > 0;
    }

    public async Task<bool> InCompleteTaskAsync(Guid taskId, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        await using var context = new TodoListContext(contextOptions);
        var result = await context.Tasks.AsNoTracking()
            .Where(x => 
                x.Id == taskId &&
                (x.TaskList.OwnerId == onBehalfOf ||
                 x.TaskList.UserLinks.Any(u => u.UserId == onBehalfOf)))
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.IsCompleted, false), 
                cancellationToken: cancellationToken);

        return result > 0;
    }
}