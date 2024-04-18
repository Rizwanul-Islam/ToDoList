using AutoMapper;
using MediatR;
using ToDoService.Application.Common.Exceptions;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.Features.Requests.Commands;
using ToDoService.Domain.Entities;

namespace ToDoService.Application.Features.Handlers.Commands
{
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
            var task = await _taskRepository.Get(request.updateTaskDto.Id);
            if (task is null)
                throw new NotFoundException(nameof(task), request.updateTaskDto.Id);

            // Update the task using factory method
            _ = ToDoTask.UpdateTask(task, request.updateTaskDto.TaskName, request.updateTaskDto.StartDate, request.updateTaskDto.EndDate, request.updateTaskDto.IsDone);

            // Update and save the task
            await _taskRepository.Update(task);
            await _taskRepository.Save();

            return Unit.Value;
        }
    }
}
