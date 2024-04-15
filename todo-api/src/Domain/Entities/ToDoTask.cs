using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoService.Domain.Common;

namespace ToDoService.Domain.Entities;
public class ToDoTask : BaseAuditableEntity
{
    public string? TaskName { get; set; }
    public DateTime StartDate { get; set; } = default;
    public DateTime EndDate { get; set; } = default;
    public bool IsDone { get; set; } = false;
}
