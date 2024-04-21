using AutoMapper;
using MediatR;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.DTOs;
using ToDoService.Application.Features.Requests.Queries;

namespace ToDoService.Application.Features.Handlers.Queries;

/// <summary>
/// Handler for the GetTaskListRequest.
/// </summary>
public class GetTaskListRequestHandler : IRequestHandler<GetTaskListRequest, List<TaskDto>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTaskListRequestHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The task repository.</param>
    /// <param name="mapper">The mapper.</param>
    public GetTaskListRequestHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetTaskListRequest asynchronously.
    /// </summary>
    /// <param name="request">The get task list request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<List<TaskDto>> Handle(GetTaskListRequest request, CancellationToken cancellationToken)
    {
        // Get all tasks from repository
        var tasks = await _taskRepository.GetAll();

        // Map tasks to TaskDto list
        var taskList = _mapper.Map<List<TaskDto>>(tasks);

        return taskList;
    }
}
