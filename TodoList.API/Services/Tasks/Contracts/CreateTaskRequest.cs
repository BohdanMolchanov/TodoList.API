using FluentValidation;

namespace TodoList.API.Services.Tasks.Contracts;

public class CreateTaskRequest
{
    public Guid? TaskListId { get; set; }
    public Guid? OnBehalfOf { get; set; }
    public string? Name { get; set; }
}

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.OnBehalfOf)
            .Must(x => x != Guid.NewGuid())
            .WithMessage("Required");
        
        RuleFor(x => x.TaskListId)
            .Must(x => x != Guid.NewGuid())
            .WithMessage("Required");
        
        RuleFor(x => x.Name)
            .Must(x => !string.IsNullOrEmpty(x))
            .MaximumLength(255)
            .MinimumLength(1);
    }
}