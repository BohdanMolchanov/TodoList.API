using FluentValidation;
using TodoList.API.Models.BasicModels;

namespace TodoList.API.Services.TaskLists.Contracts.Requests;

public class GetManyRequest : PagingRequest
{
    public Guid? OnBehalfOf { get; set; }
}

public class GetManyRequestValidator : PagingModelValidator<GetManyRequest>
{
    public GetManyRequestValidator()
    {
        RuleFor(x => x.OnBehalfOf)
            .Must(x => x.HasValue && x != Guid.Empty)
            .WithMessage("required");
    }
}