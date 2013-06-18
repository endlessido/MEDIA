using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace MEDIA.Infrastructure.Repositories
{
    /// <summary>
    /// Public DAL Interface
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class, new()
    {
        /// <summary>
        ///get DbContext
        /// </summary>
        ObjectContext DbContext { get; }

        /// <summary>
        /// Add Model
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="isDelayUpdate">isDelay to update, default is true</param>
        /// <returns>affect count</returns>
        int Add(TEntity model, bool isDelayUpdate = true);

        /// <summary>
        /// delete model
        /// </summary>
        /// <param name="where">lambda expression</param>
        /// <param name="isDelayUpdate">is Delay to update, default is true</param>
        /// <returns>affect count</returns>
        int Delete(Expression<Func<TEntity, bool>> where, bool isDelayUpdate = true);

        /// <summary>
        /// Query model by lambda expression
        /// </summary>
        /// <param name="where">lambda expression</param>
        /// <param name="isNoTracking">is directly to get datasource,default is false</param>
        /// <returns>
        /// Model Entity
        /// </returns>
        TEntity GetSingle(Expression<Func<TEntity, bool>> where, bool isNoTracking = false);

        /// <summary>
        /// Get IQueryable interface list by lambda expression
        /// </summary>
        /// <param name="where">lambda expression</param>
        /// <param name="isNoTracking">is directly to get datasource,default is false</param>
        /// <returns>
        /// IQueryable
        /// </returns>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> where, bool isNoTracking = false);

        /// <summary>
        /// Get IQueryable interface list
        /// </summary>
        /// <param name="isNoTracking">is directly to get datasource,default is false</param>
        /// <returns>
        /// IQueryable接口集合
        /// </returns>
        IQueryable<TEntity> GetQuery(bool isNoTracking = false);

        /// <summary>
        /// DataBind model list
        /// </summary>
        /// <returns>get databind model list</returns>
        BindingList<TEntity> GetBindingList();

        /// <summary>
        /// get all entity list
        /// </summary>
        /// <param name="isNoTracking">is directly to get datasource,default is false</param>
        /// <returns>
        /// entity list
        /// </returns>
        IList<TEntity> GetAll(bool isNoTracking = false);

        /// <summary>
        /// update model by ObjectContext object
        /// </summary>
        /// <param name="model">model entity</param>
        /// <param name="isDelayUpdate">is Delay to update, default is true</param>
        /// <returns>affect count</returns>
        int Update(TEntity model, bool isDelayUpdate = true);

        /// <summary>
        /// Save result in database
        /// </summary>
        /// <returns>affect count</returns>
        int SaveChanges();
    }
}
