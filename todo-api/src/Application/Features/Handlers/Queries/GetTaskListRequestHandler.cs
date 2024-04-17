using AutoMapper;
using MediatR;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.DTOs;
using ToDoService.Application.Features.Requests.Queries;

namespace ToDoService.Application.Features.Handlers.Queries;
public class GetTaskListRequestHandler : IRequestHandler<GetTaskListRequest, List<TaskDto>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTaskListRequestHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }
    public async Task<List<TaskDto>> Handle(GetTaskListRequest request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAll();
        var taskList = _mapper.Map<List<TaskDto>>(tasks);
        return taskList;
    }
}
