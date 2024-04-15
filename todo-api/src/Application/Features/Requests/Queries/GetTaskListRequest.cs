using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoService.Application.DTOs;

namespace ToDoService.Application.Features.Requests.Queries;
public class GetTaskListRequest : IRequest<List<TaskDto>>
{

}
