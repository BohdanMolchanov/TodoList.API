namespace TodoList.API.Services.Requests;

public class CreateTaskListRequestModel
{
    public required Guid OnBehalfOf { get; set; }
    public required string Name { get; set; }
}