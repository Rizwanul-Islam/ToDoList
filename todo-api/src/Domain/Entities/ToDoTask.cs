using ToDoService.Domain.Common;
using ToDoService.Domain.Exceptions;
using ToDoService.Domain.Validators;

namespace ToDoService.Domain.Entities;
public sealed class ToDoTask : BaseEntity
{
    private ToDoTask(string? taskName, DateTime? startDate, DateTime? endDate)
    {
        TaskName = taskName;
        StartDate = startDate;
        EndDate = endDate;
    }
    private ToDoTask() { }
    public string? TaskName { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public bool IsDone { get; private set; }
    public DateTime? Created { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTime? LastModified { get; private set; }

    public static ToDoTask CreateTask(string? taskName, DateTime? startDate, DateTime? endDate)
    {
        var task = new ToDoTask(taskName, startDate, endDate);
        task.Created = DateTime.Now;
        task.IsDone = false;
        ValidateTask(task);

        return task;
    }

    public static ToDoTask UpdateTask(ToDoTask task, string? newTaskName, DateTime? newStartDate, DateTime? newEndDate, bool isDone)
    {
        task.TaskName = newTaskName;
        task.StartDate = newStartDate;
        task.EndDate = newEndDate;
        task.IsDone = isDone;
        task.LastModified = DateTime.Now;

        ValidateTask(task);

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
