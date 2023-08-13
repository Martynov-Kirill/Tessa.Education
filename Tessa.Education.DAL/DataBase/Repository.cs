using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Tessa.Education.Entites.DataBase.Interfaces;
using Tessa.Education.Entites.Entities.Interfaces;

namespace Tessa.Education.DAL.DataBase
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        public readonly ApplicationDbContext _context;
        public readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        #region Implementation of IGenericRepository<T>
        /// <inheritdoc />
        public async Task<TEntity> AddAsync(TEntity entity, bool saveChanges = true)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);

            return entity;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            if (entities.Any() == false)
                throw new ArgumentNullException(nameof(entities));

            _dbSet.AddRange(entities);

            return entities;
        }

        /// <inheritdoc />
        public TEntity Update(TEntity entity, bool saveChanges = true)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);

            return entity;
        }

        /// <inheritdoc />
        public async Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true)
        {
            if (entity is null) 
                throw new ArgumentNullException(nameof(entity));
            
            _dbSet.Update(entity);
            
            return entity;
        }

        /// <inheritdoc />
        public TEntity UpdateRange(TEntity entity, bool saveChanges = true)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.UpdateRange(entity);

            return entity;
        }
        public async Task<IEnumerable<TEntity>> UpdateRange
            (IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            if (entities.Any() == false) throw new ArgumentNullException(nameof(entities));
            _dbSet.UpdateRange(entities);
            return entities;
        }

        /// <inheritdoc />
        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return (await _dbSet.ToListAsync().ConfigureAwait(false)).AsQueryable();
        }

        /// <inheritdoc />
        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetAsync(string id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == int.Parse(id));
        }

        /// <inheritdoc />
        public async Task<TEntity> FindAsync(TEntity entity)
        {
            return await _dbSet.FindAsync(entity).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public IQueryable<TEntity> List()
        {
            return _dbSet.AsQueryable();
        }

        public ApplicationDbContext Context()
        {
            return _context;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync().ConfigureAwait(false);
        }
        /// <inheritdoc />
        public async Task<TEntity> DeleteAsync(TEntity entity, bool saveChanges = true)
        {
            var item = await FindAsync(entity);

            return _dbSet.Remove(item).Entity;
        }

        /// <inheritdoc />
        public async Task<bool> IsEmpty(Expression<Func<TEntity, bool>> predicate)
        {
            var res = await _dbSet.FirstOrDefaultAsync(predicate);
            return (res!=null);
        }

        /// <inheritdoc />
        public async Task<TEntity> DeleteAsync(int id, bool saveChanges = true)
        {
            var entity = await GetAsync(id);

            return _dbSet.Remove(entity).Entity;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> DeleteManyAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            if (!entities.Any())
                throw new ArgumentNullException(nameof(entities));

            _dbSet.RemoveRange(entities);

            return entities;
        }

        public DbUpdateException CatchDbEntityValidationException(DbUpdateException exception)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var validationErrors in exception.Entries)
            {
                foreach (var validationError in validationErrors.Properties)
                {
                    stringBuilder.AppendLine(
                        $"Property: {validationError.Metadata.FieldInfo} Error: {validationError.CurrentValue}");
                }
            }

            return new DbUpdateException(stringBuilder.ToString());
        }

        #endregion
    }
}
