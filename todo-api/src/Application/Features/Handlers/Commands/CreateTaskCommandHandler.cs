using AutoMapper;
using MediatR;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.Features.Requests.Commands;
using ToDoService.Application.Responses;
using ToDoService.Domain.Entities;

namespace ToDoService.Application.Features.Handlers.Commands;
public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, BaseCommandResponse>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var task = ToDoTask.CreateTask(request.CreateTaskDto.TaskName, request.CreateTaskDto.StartDate, request.CreateTaskDto.EndDate);
        task = await _taskRepository.Add(task);
        await _taskRepository.Save();
        response.Success = true;
        response.Message = "Creation Successful";
        response.Id = task.Id;
        return response;
    }
}
