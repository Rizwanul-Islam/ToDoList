using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.DTOs;
using ToDoService.Application.Features.Requests.Queries;

namespace ToDoService.Application.Features.Handlers.Queries;
public class GetTaskDetailRequestHandler : IRequestHandler<GetTaskDetailRequest, TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTaskDetailRequestHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }
    public async Task<TaskDto> Handle(GetTaskDetailRequest request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.Get(request.Id);
        return _mapper.Map<TaskDto>(task);
    }
}
