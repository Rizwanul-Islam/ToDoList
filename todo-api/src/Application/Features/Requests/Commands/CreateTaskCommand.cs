using MediatR;
using ToDoService.Application.DTOs;
using ToDoService.Application.Responses;

namespace ToDoService.Application.Features.Requests.Commands;
public class CreateTaskCommand : IRequest<BaseCommandResponse>
{
    public CreateTaskDto CreateTaskDto { get; set; }
}
