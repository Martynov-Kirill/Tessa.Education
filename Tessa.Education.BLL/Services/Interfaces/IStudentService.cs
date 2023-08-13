using System.Linq.Expressions;

using Tessa.Education.Entites.Entities;

namespace Tessa.Education.BLL.Services.Interfaces
{
    public interface IStudentService
    {
        public Task<Student> Create(Student entity);
        public Task<ICollection<Student>> CreateMany(ICollection<Student> entitiesCollection);

        public Task<ICollection<Student>?> GetAll();
        public Task<Student> Get(int id);

        public Task<Student> Update(Student newEntity, int id);
        public Task<ICollection<Student>?> Update(ICollection<Student?> newEntities);

        public Task<ICollection<Student>?> FindBy(Expression<Func<Student?, bool>> expression);

        public Task<bool> Delete(int id);
        public Task<bool> DeleteMany(ICollection<int> ids);
    }
}