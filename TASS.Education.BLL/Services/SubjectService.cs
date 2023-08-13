using System.Linq.Expressions;

using Tessa.Education.BLL.Services.Interfaces;
using Tessa.Education.Entites.Entities;

namespace Tessa.Education.BLL.Services
{
    internal class SubjectService : ISubjectService
    {
        public Task<Subject> Create(Subject entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Subject>?> CreateMany(ICollection<Subject> entitiesCollection)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Subject>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Update(Subject entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Subject>?> Update(ICollection<Subject> entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Subject>?> FindBy(Expression<Func<Subject, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMany(ICollection<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
