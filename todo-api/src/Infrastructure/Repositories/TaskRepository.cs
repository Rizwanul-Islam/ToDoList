using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Domain.Entities;
using ToDoService.Infrastructure.Persistence;

namespace ToDoService.Infrastructure.Repositories;
public class TaskRepository : GenericRepository<ToDoTask>, ITaskRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TaskRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
