using AutoMapper;
using MediatR;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.Features.Requests.Commands;
using ToDoService.Application.Responses;
using ToDoService.Domain.Entities;

namespace ToDoService.Application.Features.Handlers.Commands;

/// <summary>
/// Handler for the CreateTaskCommand.
/// </summary>
public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, BaseCommandResponse>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTaskCommandHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The task repository.</param>
    /// <param name="mapper">The mapper.</param>
    public CreateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateTaskCommand asynchronously.
    /// </summary>
    /// <param name="request">The create task command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response indicating the result of the operation.</returns>
    public async Task<BaseCommandResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var task = ToDoTask.CreateTask(request.CreateTaskDto.TaskName, request.CreateTaskDto.StartDate, request.CreateTaskDto.EndDate);

        // Add the task to the repository and save changes
        task = await _taskRepository.Add(task);
        await _taskRepository.Save();

        // Update response properties
        response.Success = true;
        response.Message = "Creation Successful";
        response.Id = task.Id;

        return response;
    }
}
