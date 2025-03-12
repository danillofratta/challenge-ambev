using System.Collections.Generic;

namespace Ambev.Base.Domain.Repository;

/// <summary>
/// Only performs writing, save, update, delete
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface ICommandRepositoryBase<TEntity, TKey> where TEntity : class
{
    Task<TEntity> SaveAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);

    /// <summary>
    /// Execute before save. 
    /// The method can be overridden to perform validations or rules 
    /// that always need to be performed before save
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task BeforeSaveAsync(TEntity entity);

    /// <summary>
    /// Execure before save
    /// The method can be overridden to perform validations or rules 
    /// that always need to be performed after save 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task AfterSaveAsync(TEntity entity);

    /// <summary>
    /// Execute before update    /// The method can be overridden to perform validations or rules 
    /// that always need to be performed before save update    
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task BeforeUpdateAsync(TEntity entity);

    /// <summary>
    /// Execute after update
    /// The method can be overridden to perform validations or rules 
    /// that always need to be performed after update
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task AfterUpdateAsync(TEntity entity);

    /// <summary>
    /// Execute before delete
    /// The method can be overridden to perform validations or rules 
    /// that always need to be performed before delete   
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task BeforeDeleteAsync(TEntity entity);

    /// <summary>
    /// Execute after delete
    /// The method can be overridden to perform validations or rules 
    /// that always need to be performed after delete    
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task AfterDeleteAsync(TEntity entity);
}




