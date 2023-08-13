using System.Linq.Expressions;
using Tessa.Education.Entites.Entities;

namespace Tessa.Education.BLL.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<Group> Create(Group entity);
        public Task<ICollection<Group>?> CreateMany(ICollection<Group> entitiesCollection);

        public Task<ICollection<Group>?> GetAll();
        public Task<Group> Get(int id);

        public Task<Group?> BindMany(ICollection<Student> students);
        public Task<Group?> UnBindMany(ICollection<Student> students);

        public Task<Group> Update(Group entity);
        public Task<ICollection<Group>?> Update(ICollection<Group> entity);

        public Task<ICollection<Group>?> FindBy(Expression<Func<Group, bool>> expression);

        public Task Delete(int id);
        public Task DeleteMany(ICollection<int> ids);
    }
}