using Model = TodoList.API.Models.TaskListModel;
using Entity = TodoList.API.Data.Entities.TaskListEntity;


namespace TodoList.API.Extensions;

public static class TaskListModelExtensions
{
    public static Model ToModel(this Entity x) => new()
    {
        Id = x.Id,
        Name = x.Name,
        CreatedAt = x.CreatedAt,
        LastUpdatedAt = x.LastUpdatedAt,
        OwnerId = x.OwnerId,
    };
    
    public static Entity ToEntity(this Model x) => new()
    {
        Id = x.Id,
        Name = x.Name,
        CreatedAt = x.CreatedAt,
        LastUpdatedAt = x.LastUpdatedAt,
        OwnerId = x.OwnerId
    };
}