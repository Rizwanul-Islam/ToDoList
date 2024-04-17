using MediatR;
using ToDoService.Application.Common.Exceptions;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.Features.Requests.Commands;

namespace ToDoService.Application.Features.Handlers.Commands;
public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.Get(request.Id);
        if (task == null)
            throw new NotFoundException(nameof(task), request.Id);

        await _taskRepository.Delete(task);
        await _taskRepository.Save();

        return Unit.Value;
    }
}
