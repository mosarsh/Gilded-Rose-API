using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.DL.Repository
{
    /// <summary>
    /// Generic Repostory Class to support CRUD Method for every each repostory class 
    /// </summary>
    /// <typeparam name="TEntity">Entity name that needs to have all this methods</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get method with entity id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Get Async method with entity id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Get All method
        /// </summary>
        /// <returns>No parameters needed</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Get All method Aync
        /// </summary>
        /// <returns>IEnumerable of entities</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Find method. You can provid LINQ Express it. e.g. => .Find(a=>a.Id == model.Id)
        /// </summary>
        /// <param name="predicate">This is query to be provided to be executed</param>
        /// <returns>Returns IEnumerable of entities</returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// FindOne method. You can provid LINQ Express it. e.g. => .Find(a=>a.Id == model.Id)
        /// </summary>
        /// <param name="predicate">This is query to be provided to be executed</param>
        /// <returns>Return an entity</returns>
        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Asynchronous FindOne method. You can provid LINQ Express it. e.g. => .Find(a=>a.Id == model.Id)
        /// </summary>
        /// <param name="predicate">This is query to be provided to be executed</param>
        /// <returns>Return an entity</returns>
        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Add entity into database
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Update entity method
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Add range of entities to database
        /// </summary>
        /// <param name="entities">Entities to be added</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// Remove entity method
        /// </summary>
        /// <param name="entity">Entity to be removed</param>
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Remove range method
        /// </summary>
        /// <param name="entities">Entities to be removed</param>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
