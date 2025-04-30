using TodoList.API.Models.BasicModels;

namespace TodoList.API.Models;

public class UserModel : IdentifiedModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
}