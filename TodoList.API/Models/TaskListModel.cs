using TodoList.API.Models.BasicModels;

namespace TodoList.API.Models;

public class TaskListModel : IdentifiedModel
{
    public required string Name { get; set; }
    public Guid OwnerId { get; set; }
}