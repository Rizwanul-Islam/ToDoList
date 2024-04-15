using ToDoService.Application.Common.Interfaces;

namespace ToDoService.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
