using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Workshops.Domain.Common;
using Workshops.Domain.Interfaces;
using Workshops.Infrastructure.Data;

namespace Workshops.Infrastructure.Repositories;

public class Repository<TEntity>(AppDbContext context) : IRepository<TEntity>, IAsyncDisposable where TEntity : Entity
{
    public async Task<List<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await context.Set<TEntity>().FindAsync(predicate);
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return context.Set<TEntity>();
    }

    public async Task<TEntity> Insert(TEntity entity)
    {
        var entry = await context.Set<TEntity>().AddAsync(entity);
        return entry.Entity;
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }
}