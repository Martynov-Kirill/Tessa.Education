using Tessa.Education.Entites.DataBase;
using Tessa.Education.Entites.DataBase.Interfaces;
using Tessa.Education.Entites.Entities.Interfaces;

namespace Tessa.Education.DAL.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<string, object?> _repositories;
        private readonly ApplicationDbContext _context;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Implementation of IDisposable.

        /// <inheritdoc />
        public Repository<T> Factory<T>() where T : class, IEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object?>();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = RepositoryFactory.GetRepositoryInstance<T, Repository<T>>(_context);
                _repositories.Add(type, repository);
            }
            return (Repository<T>)_repositories[type];
        }

        /// <inheritdoc />
        public int Save(bool saveChanges = true)
        {
            return _context.SaveChanges(saveChanges);
        }

        /// <inheritdoc />
        public async Task<int> SaveAsync(bool saveChanges = true)
        {
            if (!saveChanges) return 0;
            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<bool> RollBack(Exception? exception = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _context.Dispose();
            GC.Collect();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
