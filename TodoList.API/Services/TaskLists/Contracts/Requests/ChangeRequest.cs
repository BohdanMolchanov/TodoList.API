using FluentValidation;

namespace TodoList.API.Services.TaskLists.Contracts.Requests;

public class ChangeRequest
{
    public Guid? TaskListId { get; set; }
    public Guid? OnBehalfOf { get; set; }
    public string? Name { get; set; }
}

public class ChangeRequestValidator : AbstractValidator<ChangeRequest>
{
    public ChangeRequestValidator()
    {
        RuleFor(x => x.OnBehalfOf)
            .Must(x => x.HasValue && x != Guid.Empty)
            .WithMessage("required");
        
        RuleFor(x => x.TaskListId)
            .Must(x => x.HasValue && x != Guid.Empty)
            .WithMessage("required");

        RuleFor(x => x.Name)
            .Must(x => !string.IsNullOrEmpty(x))
            .MaximumLength(255)
            .MinimumLength(1);
    }
}