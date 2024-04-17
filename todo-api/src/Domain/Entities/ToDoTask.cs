using FluentValidation;
using FluentValidation.Results;
using ToDoService.Domain.Common;
using ToDoService.Domain.Exceptions;
using ToDoService.Domain.Validators;

namespace ToDoService.Domain.Entities;
public sealed class ToDoTask : BaseEntity
{
    private ToDoTask()
    {

    }
    public string? TaskName { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public bool IsDone { get; private set; }
    public DateTime Created { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTime? LastModified { get; private set; }


    public static ToDoTask CreateTask(ToDoTask task)
    {
        ValidateTask(task);
        task.Created = DateTime.Now;
        task.IsDone = false;

        return task;
    }
    public static ToDoTask UpdateTask(ToDoTask task)
    {
        ValidateTask(task);
        task.Created = DateTime.Now;
        task.IsDone = false;

        return task;
    }

    public static void ValidateTask(ToDoTask task)
    {
        var validator = new CreateTaskValidator();
        var validationResult = validator.Validate(task);
        if (validationResult.IsValid == false)
            throw new DomainExceptions(validationResult.Errors);
    }
}
