namespace ToDoService.Application.DTOs;
public class UpdateTaskDto
{
    public int Id { get; set; }
    public string? TaskName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsDone { get; set; }
}
