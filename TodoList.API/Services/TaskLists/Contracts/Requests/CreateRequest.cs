using FluentValidation;

namespace TodoList.API.Services.TaskLists.Contracts.Requests;

public class CreateRequest
{
    public Guid? OnBehalfOf { get; set; }
    public string? Name { get; set; }
}

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.OnBehalfOf)
            .Must(x => x != Guid.NewGuid())
            .WithMessage("Required");
        
        RuleFor(x => x.Name)
            .Must(x => !string.IsNullOrEmpty(x))
            .MaximumLength(255)
            .MinimumLength(1);
    }
}