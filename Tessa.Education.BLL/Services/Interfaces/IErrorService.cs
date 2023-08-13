using Tessa.Education.Entites.Entities;
using Tessa.Education.Entites.Models;

namespace Tessa.Education.BLL.Services.Interfaces
{
    public interface IErrorService 
    {
        Task<Error> Create(Error error);
        Task<Error> Get(int id);
        Task<Error?> GetByMnemonicCode(string? mnemonicCode);
        Task<Error?> GetByName(string name);
        Task<Error> Update(int id, Error newError);
        Task<IEnumerable<Error>> GetAll(ErrorFilterModel errorFilterModel);
        Task Delete(int id);
    }
}
