using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoService.Application.DTOs.Common;

namespace ToDoService.Application.DTOs;
public class TaskDto : BaseDto
{
    public string? TaskName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
