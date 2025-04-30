namespace TodoList.API.Repositories.Requests;

public class ChangeTaskListRequestModel
{
    public Guid TaskListId { get; set; }
    public required string Name { get; set; }
}