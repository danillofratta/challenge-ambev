namespace Ambev.Base.Domain.Repository;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IQueryRepositoryBase<TEntity, TKey> where TEntity : class
{
    Task<TEntity> GetByIdAsync(TKey id);
    Task<IQueryable<TEntity>> GetAllAsync();
}