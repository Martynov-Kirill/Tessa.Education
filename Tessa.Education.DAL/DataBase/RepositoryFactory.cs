using Tessa.Education.Entites.DataBase;
using Tessa.Education.Entites.Entities.Interfaces;

namespace Tessa.Education.DAL.DataBase
{
    public class RepositoryFactory
    {
        public static TRepository? GetRepositoryInstance<T, TRepository>(params object[] args)
            where TRepository : Repository<T> where T : class, IEntity
        {
            return (TRepository)Activator.CreateInstance(typeof(TRepository), args)!;
        }
    }
}
