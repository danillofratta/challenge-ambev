using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Base.Domain.Repository;

/// <summary>
/// Performs reading in the database
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IQueryRepositoryBase<TEntity, TKey> where TEntity : class
{
    Task<TEntity> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
}


