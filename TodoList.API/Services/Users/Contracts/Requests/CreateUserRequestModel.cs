namespace TodoList.API.Services.Users.Contracts.Requests;

public class CreateUserRequestModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
}