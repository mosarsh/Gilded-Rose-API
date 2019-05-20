using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.DL.Repository
{
    /// <summary>
    /// Generic Interface for Repository. Mock it for Unit testing
    /// </summary>
    /// <typeparam name="TEntity">Entity name</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get method with entity id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns></returns>
        TEntity Get(int id);

        /// <summary>
        /// Get Async method with entity id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(int id);

        /// <summary>
        /// Get All method
        /// </summary>
        /// <returns>No parameters needed</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get All method Aync
        /// </summary>
        /// <returns>IEnumerable of entities</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Find method. You can provid LINQ Express it. e.g. => .Find(a=>a.Id == model.Id)
        /// </summary>
        /// <param name="predicate">This is query to be provided to be executed</param>
        /// <returns>Returns IEnumerable of entities</returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// FindOne method. You can provid LINQ Express it. e.g. => .Find(a=>a.Id == model.Id)
        /// </summary>
        /// <param name="predicate">This is query to be provided to be executed</param>
        /// <returns>Return an entity</returns>
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Asynchronous FindOne method. You can provid LINQ Express it. e.g. => .Find(a=>a.Id == model.Id)
        /// </summary>
        /// <param name="predicate">This is query to be provided to be executed</param>
        /// <returns>Return an entity</returns>
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Add entity into database
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        void Add(TEntity entity);

        /// <summary>
        /// Add range of entities to database
        /// </summary>
        /// <param name="entities">Entities to be added</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entity method
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        void Update(TEntity entity);

        /// <summary>
        /// Remove entity method
        /// </summary>
        /// <param name="entity">Entity to be removed</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Remove range method
        /// </summary>
        /// <param name="entities">Entities to be removed</param>
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
