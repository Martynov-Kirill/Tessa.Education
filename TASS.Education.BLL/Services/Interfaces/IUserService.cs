using Tessa.Education.Entites.Entities;
using Tessa.Education.Entites.Models;

namespace Tessa.Education.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> Create(User user);
        public Task<User> Get(int id);
        public Task<User> GetBy(string login);

        public Task<User> Authenticate(User user);

        Task<User> Update(int id, User user);

        void GetJwtTokenForUser(User user, TokenParameters tokenParameters);
        Task<string> RefreshUserToken(string userName, TokenParameters tokenParameters);
    }
}
