namespace TodoList.API.Models.ServiceResponse;

public class ErrorModel
{
    public required string Property { get; set; }
    public required string Message { get; set; }
}