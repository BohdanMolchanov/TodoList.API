namespace TodoList.API.Models.BasicModels;

public class IdentifiedModel : IdentifiedModel<Guid>;

public class IdentifiedModel<T>
{
    public required T Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
}