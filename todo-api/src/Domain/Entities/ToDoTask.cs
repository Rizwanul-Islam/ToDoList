using ToDoService.Domain.Common;
using ToDoService.Domain.Exceptions;
using ToDoService.Domain.Validators;

namespace ToDoService.Domain.Entities;

/// <summary>
/// Represents a ToDoTask entity.
/// </summary>
public sealed class ToDoTask : BaseEntity
{
    // Private constructor for creating a task
    private ToDoTask(string? taskName, DateTime? startDate, DateTime? endDate)
    {
        TaskName = taskName;
        StartDate = startDate;
        EndDate = endDate;
    }

    // Default constructor for EF Core
    private ToDoTask() { }

    /// <summary>
    /// Gets or sets the task name.
    /// </summary>
    public string? TaskName { get; private set; }

    /// <summary>
    /// Gets or sets the start date of the task.
    /// </summary>
    public DateTime? StartDate { get; private set; }

    /// <summary>
    /// Gets or sets the end date of the task.
    /// </summary>
    public DateTime? EndDate { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the task is done.
    /// </summary>
    public bool IsDone { get; private set; }

    /// <summary>
    /// Gets or sets the creation date of the task.
    /// </summary>
    public DateTime? Created { get; private set; }

    /// <summary>
    /// Gets or sets the user who created the task.
    /// </summary>
    public string? CreatedBy { get; private set; }

    /// <summary>
    /// Gets or sets the last modification date of the task.
    /// </summary>
    public DateTime? LastModified { get; private set; }

    /// <summary>
    /// Creates a new ToDoTask entity.
    /// </summary>
    public static ToDoTask CreateTask(string? taskName, DateTime? startDate, DateTime? endDate)
    {
        var task = new ToDoTask(taskName, startDate, endDate);
        task.Created = DateTime.Now;
        task.IsDone = false;
        ValidateTask(task);

        return task;
    }

    /// <summary>
    /// Updates an existing ToDoTask entity.
    /// </summary>
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

    /// <summary>
    /// Validates the ToDoTask entity using the TaskValidator.
    /// </summary>
    private static void ValidateTask(ToDoTask task)
    {
        var validator = new TaskValidator();
        var validationResult = validator.Validate(task);
        if (!validationResult.IsValid)
            throw new DomainExceptions(validationResult.Errors);
    }
}
