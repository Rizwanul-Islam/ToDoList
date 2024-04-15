using Microsoft.EntityFrameworkCore;
using ToDoService.Domain.Entities;

namespace ToDoService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    /// <summary>
    /// DbSet of Urls.
    /// </summary>
    public DbSet<ToDoTask> Tasks { get; set; }


    /// <summary>
    /// Save changes to the database. 
    /// </summary>
    /// <param name="cancellationToken">The cancellation token for the request.</param>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
