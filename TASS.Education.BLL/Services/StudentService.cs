using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tessa.Education.BLL.Services.Interfaces;
using Tessa.Education.BLL.Utils;
using Tessa.Education.Entites.DataBase.Interfaces;
using Tessa.Education.Entites.Entities;
using Tessa.Education.Entites.Models.Errors.Enums;

namespace Tessa.Education.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<Student> _repository;
        //private readonly IRepository<Grade> _gradesRepository;
        //private readonly IRepository<Group> _groupsRepository;
        public StudentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;

            _repository = _uow.Factory<Student>();
            //_gradesRepository = _uow.Factory<Grade>();
            //_groupsRepository = _uow.Factory<Group>();
        }

        public async Task<Student> Create(Student entity)
        {
            if (entity is null)
                throw new ErrorHandler(ErrorType.DATA_ERR.GetDescription());

            var result = await _repository.AddAsync(entity).ConfigureAwait(false);
            await _uow.SaveAsync().ConfigureAwait(false);

            return result;
        }

        public async Task<ICollection<Student>> CreateMany(ICollection<Student> entitiesCollection)
        {
            if(entitiesCollection.Any() != true)
                throw new ErrorHandler(ErrorType.FIELD_IS_EMPTY.GetDescription());

            var result = await _repository.AddManyAsync(entitiesCollection).ConfigureAwait(false);
            await _uow.SaveAsync().ConfigureAwait(false);

            return result.ToList();
        }

        public async Task<ICollection<Student>?> GetAll()
        {
            var result = await _repository.List()
                .AsNoTracking()
                .Include(x => x.Grades)
                .Include(x => x.Subjects)
                .ToListAsync()
                .ConfigureAwait(false);

            return result.ToList();
        }

        public async Task<Student> Get(int id)
        {
            var entity = await _repository.GetAsync(id).ConfigureAwait(false);

            if(entity is null)
                throw new ErrorHandler(ErrorType.OBJ_NOT_FND.GetDescription());

            return entity;
        }

        public async Task<Student> Update(Student newEntity, int id)
        {
            if (newEntity is null)
                throw new ErrorHandler(ErrorType.DATA_ERR.GetDescription());

            var entity = await Get(id).ConfigureAwait(false);

            _repository.Context().Entry(entity).CurrentValues.SetValues(newEntity);
            await _uow.SaveAsync().ConfigureAwait(false);

            return entity;
        }

        public async Task<ICollection<Student?>> Update(ICollection<Student?> entities)
        {
            if (entities.Any() != true)
                throw new ErrorHandler(ErrorType.FIELD_IS_EMPTY.GetDescription());

            var collection = await FindBy(x => entities.Select(y => x.Equals(y)).Any()).ConfigureAwait(false);

            _repository.Context().Entry(collection).CurrentValues.SetValues(entities);
            await _uow.SaveAsync().ConfigureAwait(false);

            return collection;
        }

        public async Task<ICollection<Student>?> FindBy(Expression<Func<Student?, bool>> expression)
        {
            if (expression.Body != null)
                throw new ErrorHandler(ErrorType.FIELD_IS_EMPTY.GetDescription());

            var result = await _repository.FindAsync(expression).ConfigureAwait(false);

            return result.ToList();
        }

        public async Task<bool> Delete(int id)
        {
            await _repository.DeleteAsync(id).ConfigureAwait(false);
            await _uow.SaveAsync().ConfigureAwait(false);

            return Task.CompletedTask.IsCompleted;
        }

        public async Task<bool> DeleteMany(ICollection<int> ids)
        {
            var collectionStudents = await FindBy(x => ids.Equals(x.Id)).ConfigureAwait(false);

            await _repository.DeleteManyAsync(collectionStudents).ConfigureAwait(false);
            await _uow.SaveAsync().ConfigureAwait(false);

            return Task.CompletedTask.IsCompleted;
        }
    }
}
