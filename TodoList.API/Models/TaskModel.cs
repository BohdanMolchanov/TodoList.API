using TodoList.API.Models.BasicModels;

namespace TodoList.API.Models;

public class TaskModel : IdentifiedModel
{
    public Guid TaskListId { get; set; }
    public required string Name { get; set; }
    public bool IsCompleted { get; set; }
}