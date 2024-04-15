using FluentValidation;
using ToDoService.Application.Features.Requests.Commands;

namespace ToDoService.Application.Validators;
public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        _ = RuleFor(t => t.CreateTaskDto.TaskName)
            .NotEmpty().WithMessage("Task Name is required.")
            .NotNull()
            .MinimumLength(10).WithMessage("Task Name should be atleast 10 characters.");

        _ = RuleFor(t => t.CreateTaskDto.EndDate)
            .GreaterThan(t => t.CreateTaskDto.StartDate).WithMessage("End Date must be after Start Date");
    }

}
