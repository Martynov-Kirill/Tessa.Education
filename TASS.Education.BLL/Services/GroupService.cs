using System.Linq.Expressions;
using Tessa.Education.BLL.Services.Interfaces;
using Tessa.Education.Entites.Entities;

namespace Tessa.Education.BLL.Services
{
    internal class GroupService : IGroupService
    {
        public Task<Group> Create(Group entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Group>?> CreateMany(ICollection<Group> entitiesCollection)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Group>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Group> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Group?> BindMany(ICollection<Student> students)
        {
            throw new NotImplementedException();
        }

        public Task<Group?> UnBindMany(ICollection<Student> students)
        {
            throw new NotImplementedException();
        }

        public Task<Group> Update(Group entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Group>?> Update(ICollection<Group> entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Group>?> FindBy(Expression<Func<Group, bool>> expression)
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
