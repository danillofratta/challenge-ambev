
using Ambev.DeveloperEvaluation.Base.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Base.Infrastructure.Query.Orm.Repository;

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

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }
}