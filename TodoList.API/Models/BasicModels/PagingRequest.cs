using FluentValidation;

namespace TodoList.API.Models.BasicModels;

public class PagingRequest
{
    public int Limit { get; set; } = 30;
    public int Skip { get; set; }
}


public class PagingModelValidator<T> : AbstractValidator<T>
    where T : PagingRequest
{
    public PagingModelValidator(int maxLimit = 50)
    {
        RuleFor(x => x.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(maxLimit);

        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0);
    }
}