using ToDoService.Application.DTOs.Common;

namespace ToDoService.Application.DTOs;
public class TaskDto : BaseDto
{
    public string? TaskName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsDone { get; set; }
}
