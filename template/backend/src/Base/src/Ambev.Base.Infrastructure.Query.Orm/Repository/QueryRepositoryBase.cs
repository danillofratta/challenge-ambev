using Ambev.Base.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Base.Infrastructure.Query.Orm.Repository;

public abstract class QueryRepositoryBase<TEntity, TKey> : IQueryRepositoryBase<TEntity, TKey>
    where TEntity : class
{
    protected readonly DbContext _dbContext;

    protected QueryRepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TEntity> GetByIdAsync(TKey id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<IQueryable<TEntity>> GetAllAsync()
    {        
        return await Task.FromResult(_dbContext.Set<TEntity>().AsNoTracking().AsQueryable());
    }
}