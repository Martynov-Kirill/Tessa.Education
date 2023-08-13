using System.Linq.Expressions;
using Tessa.Education.BLL.Services.Interfaces;
using Tessa.Education.Entites.Entities;

namespace Tessa.Education.BLL.Services
{
    internal class GradeService : IGradeService
    {
        public Task<Grade> Create(Grade entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Grade>?> CreateMany(ICollection<Grade> entitiesCollection)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Grade>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Grade> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Grade> Update(Grade entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Grade>?> UpdateMany(ICollection<Grade> entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Grade>?> FindBy(Expression<Func<Grade, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMany(ICollection<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
