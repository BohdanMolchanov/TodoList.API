using FluentValidation;

namespace TodoList.API.Services.TaskLists.Contracts.Requests;

public class ChangeTaskListAccessRequestModel
{
    public Guid Id { get; set; }
    public Guid? OnBehalfOf { get; set; }
    public Guid? UserId { get; set; }
}

public class ChangeTaskListAccessRequestModelValidator : AbstractValidator<ChangeTaskListAccessRequestModel>
{
    public ChangeTaskListAccessRequestModelValidator()
    {
        RuleFor(x => x.Id)
            .Must(x => x != Guid.Empty)
            .WithMessage("required");
        
        RuleFor(x => x.OnBehalfOf)
            .Must(x => x.HasValue && x != Guid.Empty)
            .WithMessage("required");
        
        RuleFor(x => x.UserId)
            .Must(x => x.HasValue && x != Guid.Empty)
            .WithMessage("required");
    }
}