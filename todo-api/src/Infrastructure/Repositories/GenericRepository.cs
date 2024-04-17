﻿using Microsoft.EntityFrameworkCore;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Infrastructure.Persistence;

namespace ToDoService.Infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> Add(T entity)
    {
        await _dbContext.AddAsync(entity);
        return entity;
    }

    public async Task Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await Get(id);
        return entity != null;
    }

    public async Task<T> Get(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task Save()
    {
        _ = await _dbContext.SaveChangesAsync();
    }


}
