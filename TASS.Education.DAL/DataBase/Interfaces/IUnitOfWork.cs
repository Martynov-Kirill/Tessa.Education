using Tessa.Education.DAL.DataBase;
using Tessa.Education.Entites.Entities.Interfaces;

namespace Tessa.Education.Entites.DataBase.Interfaces
{
    public interface IUnitOfWork
    {
        Repository<TEntity> Factory<TEntity>() where TEntity : class, IEntity;

        int Save(bool saveChanges = true);

        Task<int> SaveAsync(bool saveChanges = true);

        Task<bool> RollBack(Exception? exception = default);

    }
}
