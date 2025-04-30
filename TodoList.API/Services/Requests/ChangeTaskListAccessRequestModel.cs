namespace TodoList.API.Services.Requests;

public class ChangeTaskListAccessRequestModel
{
    public required Guid Id { get; set; }
    public required Guid OnBehalfOf { get; set; }
    public required Guid UserId { get; set; }
}