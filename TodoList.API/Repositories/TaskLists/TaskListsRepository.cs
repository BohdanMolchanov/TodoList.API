using Microsoft.EntityFrameworkCore;
using Npgsql;
using TodoList.API.Data;
using TodoList.API.Extensions;
using TodoList.API.Models;
using TodoList.API.Repositories.Requests;

namespace TodoList.API.Repositories.TaskLists;

public class TaskListsRepository(DbContextOptions<TodoListContext> contextOptions) : ITaskListsRepository
{
    public async Task CreateTaskListAsync(TaskListModel model, CancellationToken cancellationToken)
    {
        var entity = model.ToEntity();
        await using var context = new TodoListContext(contextOptions);
        context.TaskLists.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task ChangeTaskListAsync(Guid onBehalfOf, ChangeTaskListRequestModel request, CancellationToken cancellationToken)
    {
        await using var context = new TodoListContext(contextOptions);
        await context.TaskLists.AsNoTracking()
            .Where(x => 
                x.Id == request.TaskListId &&
                (x.OwnerId == onBehalfOf ||
                 x.UserLinks.Any(u => u.UserId == onBehalfOf)))
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, request.Name)
                    .SetProperty(p => p.LastUpdatedAt, DateTime.UtcNow), 
                cancellationToken: cancellationToken);
    }

    public async Task RemoveTaskListAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken)
    {
        await using var context = new TodoListContext(contextOptions);
        await context.TaskLists.AsNoTracking()
            .Where(x => 
                x.Id == id && x.OwnerId == onBehalfOf)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.RemovedAt, DateTime.UtcNow)
                    .SetProperty(p => p.LastUpdatedAt, DateTime.UtcNow), 
                cancellationToken: cancellationToken);
    }

    public async Task<(List<TaskListModel> data, bool hasNext)> GetManyTaskListsAsync(Guid onBehalfOf, int skip, int limit, CancellationToken cancellationToken)
    {
        await using var context = new TodoListContext(contextOptions);
        var entities = await context.TaskLists.AsNoTracking()
            .Where(x =>
                (x.OwnerId == onBehalfOf ||
                 x.UserLinks.Any(u => u.UserId == onBehalfOf)))
            .OrderByDescending(x => x.CreatedAt)
            .Skip(skip)
            .Take(limit + 1)
            .ToListAsync(cancellationToken);

        var result = entities.Take(limit).Select(x => x.ToModel()).ToList();
        
        return (result, entities.Count > limit);
    }

    public async Task ShareTaskListAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        await using var context = new TodoListContext(contextOptions);

        context.TaskListToUserLinks.Add(new()
        {
            UserId = userId,
            TaskListId = id
        });

        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when ((ex.InnerException as PostgresException)?.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            //ignore
        }
    }

    public async Task RestrictTaskListAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        await using var context = new TodoListContext(contextOptions);

        await context.TaskListToUserLinks.Where(x => x.UserId == userId && x.TaskListId == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<List<UserModel>> GetTaskListUsersAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var context = new TodoListContext(contextOptions);

        var result = await context.Users.AsNoTracking()
            .Where(x => x.TaskListLinks.Any(y => y.TaskListId == id) || x.OwnedTasks.Any(o => o.Id == id))
            .ToListAsync(cancellationToken: cancellationToken);
        
        return result.Select(x => x.ToModel()).ToList();
    }

    public async Task<bool> HasTaskListAccessAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken = default)
    {
        await using var context = new TodoListContext(contextOptions);
        return await context.TaskLists.AsNoTracking()
            .Where(x =>
                x.Id == id &&
                (x.OwnerId == onBehalfOf ||
                 x.UserLinks.Any(u => u.UserId == onBehalfOf))).AnyAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> HasTaskListOwnerAccessAsync(Guid id, Guid onBehalfOf, CancellationToken cancellationToken = default)
    {
        await using var context = new TodoListContext(contextOptions);
        return await context.TaskLists.AsNoTracking()
            .Where(x =>
                x.Id == id &&
                x.OwnerId == onBehalfOf).AnyAsync(cancellationToken);
    }
}