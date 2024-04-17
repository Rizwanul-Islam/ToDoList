using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.Features.Requests.Commands;
using ToDoService.Application.Responses;
using ToDoService.Application.Validators;
using ToDoService.Domain.Entities;

namespace ToDoService.Application.Features.Handlers.Commands;
public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Unit>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.Get(request.TaskDto.Id);
        _ = _mapper.Map(request.TaskDto, task);
        task = ToDoTask.UpdateTask(task);
        await _taskRepository.Update(task);
        await _taskRepository.Save();

        return Unit.Value;
    }
}
