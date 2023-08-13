using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using Tessa.Education.BLL.Models;
using Tessa.Education.BLL.Services.Interfaces;
using Tessa.Education.Entites.DataBase.Interfaces;
using Tessa.Education.Entites.Entities;
using Tessa.Education.Entites.Models;
using Tessa.Education.Entites.Models.Errors;

namespace Tessa.Education.BLL.Services
{
    public class ErrorService : IErrorService
    {
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Error>? _repository;
        public ErrorService(IUnitOfWork uow, IOptions<ErrorConfig> errorServiceApiSettings)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _repository = _uow.Factory<Error>();

            _httpClient = new HttpClient();
        }

        private async Task ValidateError(Error error)
        {
            var errorWithSameErrorCode = (await _repository.FindAsync(e => e.MnemonicCode == error.MnemonicCode))
                .FirstOrDefault();
            if (errorWithSameErrorCode != null && errorWithSameErrorCode.Id != error.Id)
                throw new AlreadyExistsException($"Error with error code {error.MnemonicCode} already exists");

            var errorWithSameName = (await _repository.FindAsync(e => e.Name == error.Name)).FirstOrDefault();

            if (errorWithSameName != null && errorWithSameName.Id != error.Id)
                throw new AlreadyExistsException($"Error with name {error.Name} already exists");

            var errorWithSameMnemonicCode =
                (await _repository.FindAsync(
                    e => e.MnemonicCode == error.MnemonicCode)).FirstOrDefault();

            if (errorWithSameMnemonicCode != null && errorWithSameMnemonicCode.Id != error.Id)
                throw new AlreadyExistsException($"Error with mnemonic code {error.MnemonicCode} already exists");
        }
        public async Task<Error> Create(Error error)
        {
            await ValidateError(error);
            error.MnemonicCode = error.MnemonicCode;

            var result = await _repository.AddAsync(error);
            await _uow.SaveAsync().ConfigureAwait(false);
            return result;
        }

        public async Task<Error> Get(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<Error?> GetByMnemonicCode(string? code)
        {
            var upperCaseMnemonicCode = code.ToUpper();
            return (await _repository.FindAsync(e => e.MnemonicCode == upperCaseMnemonicCode))
                .FirstOrDefault();
        }

        public async Task<Error?> GetByName(string name)
        {
            return (await _repository.FindAsync(e => e.Name == name)).FirstOrDefault();
        }

        public async Task<Error> Update(int id, Error entity)
        {
            var current = await _repository.GetAsync(id);
            if (current is null)
                throw new DoesNotExistException("Error with id {id} does not exist");

            await ValidateError(entity);
            entity.Id = current.Id;

            _repository.Context().Entry(current).CurrentValues.SetValues(entity);
            await _uow.SaveAsync().ConfigureAwait(false);

            return entity;
        }

        public async Task<IEnumerable<Error>> GetAll(ErrorFilterModel errorFilterModel)
        {
            Expression<Func<Error, bool>> filter = error => true;
            if (!string.IsNullOrWhiteSpace(errorFilterModel.Name))
                filter = ExpressionBuilder.AndFilterExpression(filter,
                    error => error.Name.Contains(errorFilterModel.Name));

            if (!string.IsNullOrWhiteSpace(errorFilterModel.MnemonicCode))
                filter = ExpressionBuilder.AndFilterExpression(filter,
                    error => error.MnemonicCode.Contains(errorFilterModel.MnemonicCode));

            return await _repository.FindAsync(filter);
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
            await _uow.SaveAsync().ConfigureAwait(false);
        }
    }
}
