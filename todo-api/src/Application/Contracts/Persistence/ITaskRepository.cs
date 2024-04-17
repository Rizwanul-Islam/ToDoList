using ToDoService.Domain.Entities;

namespace ToDoService.Application.Contracts.Persistence;
public interface ITaskRepository : IGenericRepository<ToDoTask>
{

}
