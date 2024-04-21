using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoService.Application.Common.Interfaces;
using ToDoService.Domain.Entities;

namespace ToDoService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    public DbSet<ToDoTask> Tasks { get; set; }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator
        )
        : base(options)
    {
        _mediator = mediator;
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
