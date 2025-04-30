using Model = TodoList.API.Models.TaskModel;
using Entity = TodoList.API.Data.Entities.TaskEntity;


namespace TodoList.API.Extensions;

public static class TaskModelExtensions
{
    public static Model ToModel(this Entity x) => new()
    {
        Id = x.Id,
        Name = x.Name,
        CreatedAt = x.CreatedAt,
        IsCompleted = x.IsCompleted,
        LastUpdatedAt = x.LastUpdatedAt,
        TaskListId = x.TaskListId,
    };
    
    public static Entity ToEntity(this Model x) => new()
    {
        Id = x.Id,
        Name = x.Name,
        CreatedAt = x.CreatedAt,
        IsCompleted = x.IsCompleted,
        LastUpdatedAt = x.LastUpdatedAt,
        TaskListId = x.TaskListId,
    };
}