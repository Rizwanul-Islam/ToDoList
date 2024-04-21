using MediatR;
using ToDoService.Application.Common.Exceptions;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.Features.Requests.Commands;

namespace ToDoService.Application.Features.Handlers.Commands;

/// <summary>
/// Handler for the DeleteTaskCommand.
/// </summary>
public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTaskCommandHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The task repository.</param>
    public DeleteTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Handles the DeleteTaskCommand asynchronously.
    /// </summary>
    /// <param name="request">The delete task command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        // Get the task to delete
        var task = await _taskRepository.Get(request.Id);

        // If task not found, throw NotFoundException
        if (task == null)
            throw new NotFoundException(nameof(task), request.Id);

        // Delete the task and save changes
        await _taskRepository.Delete(task);
        await _taskRepository.Save();

        // Return success
        return Unit.Value;
    }
}
