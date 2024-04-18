using MediatR;
using ToDoService.Application.DTOs;

namespace ToDoService.Application.Features.Requests.Commands;
public class UpdateTaskCommand : IRequest<Unit>
{
    public UpdateTaskDto updateTaskDto { get; set; }
}
