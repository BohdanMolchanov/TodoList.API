using FluentValidation.Results;

namespace TodoList.API.Models.ServiceResponse;

public class ServiceResponse<T> : ServiceResponse
{
    public ServiceResponse()
    {
        
    }

    public ServiceResponse(string propertyName, string errorMessage) : base(propertyName, errorMessage)
    {
        
    }
    
    public ServiceResponse(ValidationResult validationResult) : base(validationResult)
    {
        
    }
    
    public T? Result { get; set; }
}

public class ServiceResponse
{
    public ServiceResponse(string propertyName, string errorMessage)
    {
        Errors =
        [
            new()
            {
                Message = errorMessage,
                Property = propertyName
            }
        ];
    }
    public ServiceResponse()
    {
        
    }
    public ServiceResponse(ValidationResult validationResult)
    {
        Errors = validationResult.Errors.Select(x => new ErrorModel
        {
            Message = x.ErrorMessage,
            Property = x.PropertyName
        }).ToList();
    }
    
    public List<ErrorModel> Errors { get; set; } = new();
    public bool IsSuccess => Errors.Count == 0;
}