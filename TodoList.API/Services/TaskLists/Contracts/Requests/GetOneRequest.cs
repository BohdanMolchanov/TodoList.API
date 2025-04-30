using FluentValidation;

namespace TodoList.API.Services.TaskLists.Contracts.Requests;

public class GetOneRequest
{
    public Guid? TaskListId { get; set; }
    public Guid? OnBehalfOf { get; set; }
}

public class GetOneRequestValidator : AbstractValidator<GetOneRequest>
{
    public GetOneRequestValidator()
    {
        RuleFor(x => x.OnBehalfOf)
            .Must(x => x.HasValue && x != Guid.Empty)
            .WithMessage("required");
        
        RuleFor(x => x.TaskListId)
            .Must(x => x.HasValue && x != Guid.Empty)
            .WithMessage("required");
    }
}