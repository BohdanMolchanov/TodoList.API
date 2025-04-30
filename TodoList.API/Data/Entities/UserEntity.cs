using TodoList.API.Data.Entities.BasicEntities;

namespace TodoList.API.Data.Entities;

public class UserEntity : IdentifiedEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public List<TaskListToUserLinkEntity> TaskListLinks { get; set; } = new();
    public List<TaskListEntity> OwnedTasks { get; set; } = new();
}

public class UserEntityConfiguration : IdentifiedEntityConfiguration<UserEntity>;