namespace Ambev.Base.Domain.Repository;

/// <summary>
/// Only execute queries
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IQueryRepositoryBase<TEntity, TKey> where TEntity : class
{
    /// <summary>
    /// Get by default entity id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(TKey id);

    /// <summary>
    /// Get all 
    /// </summary>
    /// <returns></returns>
    Task<IQueryable<TEntity>> GetAllAsync();
}