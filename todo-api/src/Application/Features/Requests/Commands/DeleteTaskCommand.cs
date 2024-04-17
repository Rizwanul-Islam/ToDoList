using MediatR;

namespace ToDoService.Application.Features.Requests.Commands;
public class DeleteTaskCommand : IRequest
{
    public int Id { get; set; }
}
