using AutoMapper;
using MediatR;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.DTOs;
using ToDoService.Application.Features.Requests.Queries;

namespace ToDoService.Application.Features.Handlers.Queries;

/// <summary>
/// Handler for the GetTaskDetailRequest.
/// </summary>
public class GetTaskDetailRequestHandler : IRequestHandler<GetTaskDetailRequest, TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTaskDetailRequestHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The task repository.</param>
    /// <param name="mapper">The mapper.</param>
    public GetTaskDetailRequestHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetTaskDetailRequest asynchronously.
    /// </summary>
    /// <param name="request">The get task detail request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<TaskDto> Handle(GetTaskDetailRequest request, CancellationToken cancellationToken)
    {
        // Get the task by id
        var task = await _taskRepository.Get(request.Id);

        // Map the task entity to TaskDto and return
        return _mapper.Map<TaskDto>(task);
    }
}
