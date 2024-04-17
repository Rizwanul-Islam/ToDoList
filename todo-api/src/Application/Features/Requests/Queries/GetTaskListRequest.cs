using MediatR;
using ToDoService.Application.DTOs;

namespace ToDoService.Application.Features.Requests.Queries;
public class GetTaskListRequest : IRequest<List<TaskDto>>
{

}
