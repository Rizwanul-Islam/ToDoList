using Microsoft.EntityFrameworkCore;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Infrastructure.Persistence;

namespace ToDoService.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation for CRUD operations.
/// </summary>
/// <typeparam name="T">The type of entity.</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
    /// </summary>
    /// <param name="dbContext">The application's database context.</param>
    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<T> Add(T entity)
    {
        _ = await _dbContext.AddAsync(entity);
        return entity;
    }

    /// <inheritdoc/>
    public async Task Delete(T entity)
    {
        _ = _dbContext.Set<T>().Remove(entity);
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(int id)
    {
        var entity = await Get(id);
        return entity != null;
    }

    /// <inheritdoc/>
    public async Task<T> Get(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    /// <inheritdoc/>
    public async Task Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    /// <inheritdoc/>
    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}
