using MediatR;
using ToDoService.Application.DTOs;

namespace ToDoService.Application.Features.Requests.Queries;
public class GetTaskDetailRequest : IRequest<TaskDto>
{
    public int Id { get; set; }
}
