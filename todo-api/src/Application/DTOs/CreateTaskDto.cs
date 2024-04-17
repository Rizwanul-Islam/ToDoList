namespace ToDoService.Application.DTOs;
public class CreateTaskDto
{
    public string? TaskName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
