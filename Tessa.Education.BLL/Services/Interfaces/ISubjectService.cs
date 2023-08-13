using System.Linq.Expressions;
using Tessa.Education.Entites.Entities;

namespace Tessa.Education.BLL.Services.Interfaces
{
    public interface ISubjectService
    {
        public Task<Subject> Create(Subject entity);
        public Task<ICollection<Subject>?> CreateMany(ICollection<Subject> entitiesCollection);

        public Task<ICollection<Subject>> GetAll();
        public Task<Subject> Get(int id);

        public Task<Subject> Update(Subject entity);
        public Task<ICollection<Subject>?> Update(ICollection<Subject> entity);

        public Task<ICollection<Subject>?> FindBy(Expression<Func<Subject, bool>> expression);

        public Task<bool> Delete(int id);
        public Task<bool> DeleteMany(ICollection<int> ids);

    }
}
