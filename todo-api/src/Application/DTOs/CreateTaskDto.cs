using System;
namespace ToDoService.Application.DTOs;
public class CreateTaskDto
{
    public string? TaskName { get; set; } = default;
    public DateTime StartDate { get; set; } = default;
    public DateTime EndDate { get; set; } = default;
}
