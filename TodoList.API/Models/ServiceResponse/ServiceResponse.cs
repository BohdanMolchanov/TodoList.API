namespace TodoList.API.Models.ServiceResponse;

public class ServiceResponse<T> : ServiceResponse
{
    public T? Result { get; set; }
}

public class ServiceResponse
{
    public List<ErrorModel> Errors { get; set; } = new();
    public bool IsSuccess => Errors.Count == 0;
}