using FluentValidation;
using ToDoService.Application.DTOs;
using ToDoService.Application.Features.Requests.Commands;

namespace ToDoService.Application.Validators;
public class UpdateTaskValidator : AbstractValidator<TaskDto>
{
    public UpdateTaskValidator()
    {
        _ = RuleFor(t => t.TaskName)
            .NotEmpty().WithMessage("Task Name is required.")
            .NotNull()
            .MinimumLength(10).WithMessage("Task Name should be atleast 10 characters.");

        _ = RuleFor(t => t.Id).NotNull().WithMessage("Task Id must be provided");

    }

}
