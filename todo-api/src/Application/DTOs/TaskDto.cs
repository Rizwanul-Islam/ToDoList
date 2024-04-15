using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoService.Application.DTOs.Common;

namespace ToDoService.Application.DTOs;
public class TaskDto : BaseDto
{
    public string? TaskName { get; set; } = default;
    public DateTime StartDate { get; set; } = default;
    public DateTime EndDate { get; set; } = default;
    public bool IsDone { get; set; } = false;
}
