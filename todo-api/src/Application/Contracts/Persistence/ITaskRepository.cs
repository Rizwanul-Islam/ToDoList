using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoService.Domain.Entities;

namespace ToDoService.Application.Contracts.Persistence;
public interface ITaskRepository : IGenericRepository<ToDoTask>
{

}
