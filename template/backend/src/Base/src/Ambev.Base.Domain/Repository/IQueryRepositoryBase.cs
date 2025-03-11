using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Base.Domain.Repository;

/// <summary>
/// Could be in separate project
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IQueryRepositoryBase<TEntity, TKey> where TEntity : class
{
    Task<TEntity> GetByIdAsync(TKey id);
    Task<IQueryable<TEntity>> GetAllAsync();
}