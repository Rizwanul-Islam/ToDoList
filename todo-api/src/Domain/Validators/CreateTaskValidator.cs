using FluentValidation;
using ToDoService.Domain.Entities;

namespace ToDoService.Domain.Validators;
public class CreateTaskValidator : AbstractValidator<ToDoTask>
{
    public CreateTaskValidator()
    {
        _ = RuleFor(t => t.TaskName)
            .NotEmpty().WithMessage("Task Name is required.")
            .NotNull()
            .MinimumLength(10).WithMessage("Task Name should be atleast 10 characters.");

        _ = RuleFor(t => t.EndDate)
            .GreaterThan(t => t.StartDate).WithMessage("End Date must be after Start Date");
    }

}
