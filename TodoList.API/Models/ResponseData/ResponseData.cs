using TodoList.API.Models.ServiceResponse;

namespace TodoList.API.Models.ResponseData;

public class ResponseData<T> : ResponseData
{
    public T? Data { get; set; }
}

public class ResponseData
{
    public List<ErrorModel>? Errors { get; set; }
    public Meta? Meta { get; set; }
}