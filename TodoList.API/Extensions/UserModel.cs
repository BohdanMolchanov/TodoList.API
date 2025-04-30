using Model = TodoList.API.Models.UserModel;
using Entity = TodoList.API.Data.Entities.UserEntity;

namespace TodoList.API.Extensions;

public static class UserModelExtensions
{
    public static Model ToModel(this Entity x) => new()
    {
        Id = x.Id,
        FirstName = x.FirstName,
        LastName = x.LastName,
        MiddleName = x.MiddleName,
        CreatedAt = x.CreatedAt,
        LastUpdatedAt = x.LastUpdatedAt,
    };
    
    public static Entity ToEntity(this Model x) => new()
    {
        Id = x.Id,
        FirstName = x.FirstName,
        LastName = x.LastName,
        MiddleName = x.MiddleName,
        CreatedAt = x.CreatedAt,
        LastUpdatedAt = x.LastUpdatedAt,
    };
}