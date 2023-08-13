using System.Linq.Expressions;
using Tessa.Education.Entites.Entities;

namespace Tessa.Education.BLL.Services.Interfaces
{
    public interface IGradeService
    {
        public Task<Grade> Create(Grade entity);
        public Task<ICollection<Grade>?> CreateMany(ICollection<Grade> entitiesCollection);

        public Task<ICollection<Grade>?> GetAll();
        public Task<Grade> Get(int id);
        
        public Task<Grade> Update(Grade entity);
        public Task<ICollection<Grade>?> UpdateMany(ICollection<Grade> entity);

        public Task<ICollection<Grade>?> FindBy(Expression<Func<Grade, bool>> expression);

        public Task Delete(int id);
        public Task DeleteMany(ICollection<int> ids);
    }
}