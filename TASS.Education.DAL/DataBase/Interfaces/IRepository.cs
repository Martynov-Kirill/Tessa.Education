using System.Linq.Expressions;
using Tessa.Education.DAL.DataBase;
using Tessa.Education.Entites.Entities.Interfaces;

namespace Tessa.Education.Entites.DataBase.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Add 'entity' to DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <param name="entity">Item</param>
        /// <param name="saveChanges">status</param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity, bool saveChanges = true);

        /// <summary>
        /// Add 'entities' to DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <param name="entities">collection of Items</param>
        /// <param name="saveChanges">status</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities, bool saveChanges = true);

        /// <summary>
        /// Update data 'entity' from DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <param name="entity">new entity</param>
        /// <param name="saveChanges">status</param>
        /// <returns>updated entity</returns>
        TEntity Update(TEntity entity, bool saveChanges = true);

        /// <summary>
        /// Update async data 'entity' from DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <param name="entity">new entity</param>
        /// <param name="saveChanges">status</param>
        /// <returns>updated entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true);

        /// <summary>
        /// Update data 'entity' from DataBase(<typeparamref name="TEntity"/>) by context
        /// Begins tracking the given entities and entries reachable from the given entities
        /// Generally, no database interaction will be performed until SaveChanges() called
        /// </summary>
        /// <param name="entity">new entity</param>
        /// <param name="saveChanges">status</param>
        /// <returns>updated entity</returns>
        TEntity UpdateRange(TEntity entity, bool saveChanges = true);

        /// <summary>
        /// Update data collection 'entity' from DataBase(<typeparamref name="TEntity"/>) by context
        /// Begins tracking the given entities and entries reachable from the given entities
        /// Generally, no database interaction will be performed until SaveChanges() called
        /// </summary>
        /// <param name="entity">new entity</param>
        /// <param name="saveChanges">status</param>
        /// <returns>updated entity</returns>
        Task<IEnumerable<TEntity?>> UpdateRange(IEnumerable<TEntity?> entities, bool saveChanges = true);

        /// <summary>
        /// Get all 'entities' from DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <returns>Query of <typeparamref name="TEntity"/></returns>
        Task<IQueryable<TEntity>> GetAllAsync();

        /// <summary>
        /// Get entity from DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Item</returns>
        Task<TEntity> GetAsync(int id);

        /// <summary>
        /// Get entity from DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Item</returns>
        Task<TEntity> GetAsync(string id);

        /// <summary>
        /// Get find entity from DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <param name="TEntity">entity</param>
        /// <returns>Found Item</returns>
        Task<TEntity> FindAsync(TEntity entity);

        /// <summary>
        /// Get current collection from DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <returns>Collection of TEntity</returns>
        IQueryable<TEntity> List();

        /// <summary>
        /// Get current TEntity context (<typeparamref name="TEntity"/>) 
        /// </summary>
        /// <returns>ApplicationDbContext of TEntity</returns>
        public ApplicationDbContext Context();

        /// <summary>
        /// Get find entity from DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <param name="predicate">condition expression</param>
        /// <returns>Found Item</returns>
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity?, bool>> predicate);

        Task<bool> IsEmpty(Expression<Func<TEntity?, bool>> predicate);

        /// <summary>
        /// Remove founded 'entity' from DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <param name="saveChanges">status</param>
        /// <returns>Is success full operation</returns>
        Task<TEntity> DeleteAsync(TEntity entity, bool saveChanges = true);

        /// <summary>
        /// Remove founded 'entity' from DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <param name="saveChanges">status</param>
        /// <returns>Is success full operation</returns>
        Task<TEntity> DeleteAsync(int id, bool saveChanges = true);

        /// <summary>
        /// Remove founded 'entities' from DataBase(<typeparamref name="TEntity"/>) by context 
        /// </summary>
        /// <param name="entities">list <typeparamref name="TEntity"/></param>
        /// <param name="saveChanges">status</param>
        /// <returns>Is success full operation</returns>
        Task<IEnumerable<TEntity>> DeleteManyAsync(IEnumerable<TEntity?> entities, bool saveChanges = true);
    }
}
