using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoService.Application.Responses;
using ToDoService.Application.DTOs;

namespace ToDoService.Application.Features.Requests.Commands;
public class UpdateTaskCommand : IRequest<Unit>
{
    public TaskDto TaskDto { get; set; }
}
