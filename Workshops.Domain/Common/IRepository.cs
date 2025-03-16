namespace Workshops.Domain.Interfaces;

public interface IRepository<TEntity> 
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);

    IQueryable<TEntity> GetQueryable();
    
    Task<TEntity> Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    
    Task SaveChangesAsync();
}