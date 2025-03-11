using System.Collections.Generic;

namespace Ambev.Base.Domain.Repository;

    /// <summary>
    /// Could be in separate project
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface ICommandRepositoryBase<TEntity, TKey> where TEntity : class
{
    Task<TEntity> SaveAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);

    // Hooks opcionais para antes/depois das operações
    Task BeforeSaveAsync(TEntity entity);
    Task AfterSaveAsync(TEntity entity);
    Task BeforeUpdateAsync(TEntity entity);
    Task AfterUpdateAsync(TEntity entity);
    Task BeforeDeleteAsync(TEntity entity);
    Task AfterDeleteAsync(TEntity entity);
}




