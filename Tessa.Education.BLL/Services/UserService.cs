using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tessa.Education.BLL.Models;
using Tessa.Education.BLL.Services.Interfaces;
using Tessa.Education.Entites.DataBase.Interfaces;
using Tessa.Education.Entites.Entities;
using Tessa.Education.Entites.Models;
using Tessa.Education.Entites.Models.Errors.Enums;

namespace Tessa.Education.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private AppSettings _appSettings;
        private IRepository<User> _repository;
        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _repository = _uow.Factory<User>();
        }

        public async Task<User> Create(User user)
        {
            // return null if user not found
            if (user is null)
                throw new ErrorHandler(Enum.GetName(ErrorType.OBJ_NOT_FND));

            user.Activity.CreatedDate = DateTime.Now;
            return await _repository.AddAsync(user).ConfigureAwait(false);
        }

        public async Task<User> Get(int id)
        {
           return await _repository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task<User> GetBy(string login)
        {
            if(string.IsNullOrEmpty(login))
                throw new ErrorHandler(Enum.GetName(ErrorType.FIELD_IS_EMPTY));

            var user = await _repository.List()
                .Where(x => x.Login.Equals(login))
                .FirstOrDefaultAsync();

            // return null if user not found
            if (user is null)
                throw new ErrorHandler(Enum.GetName(ErrorType.OBJ_NOT_FND));

            return user;
        }

        public async Task<User> Authenticate(User model)
        {
            var user = (await _repository
                    .FindAsync(u => u.Login.Equals(model.Login)))
                .FirstOrDefault();

            // return null if user not found
            if (user is null)
                throw new ErrorHandler(Enum.GetName(ErrorType.OBJ_NOT_FND));

            var userHashedPassport = user.Password;

            //VerifyHashedPassport
            var verificator = new PasswordHasher();
            var verficationRsult = verificator.VerifyHashedPassword(userHashedPassport, model.Password);

            if (verficationRsult != PasswordVerificationResult.Success)
                throw new ErrorHandler(Enum.GetName(ErrorType.UNK_ERR));

            return user;
        }

        public async Task<User> Update(int id, User model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var entity = (await _repository
                    .FindAsync(x => x.Id.Equals(id))
                    .ConfigureAwait(false))
                .FirstOrDefault();

            if (model.Activity is null)
                model.Activity = new();

            model.Activity.ModifiedDate = DateTime.Now.ToUniversalTime();

            _repository.Context().Entry(entity).CurrentValues.SetValues(model);
            await _uow.SaveAsync().ConfigureAwait(false);

            return entity;
        }

        public void GetJwtTokenForUser(User user, TokenParameters tokenParameters)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // authentication successful so generate jwt token
            JwtSecurityTokenHandler tokenHandler;
            SecurityToken token;
            tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Login),
                new(Constants.LOGIN, user.Login),
                new(Constants.UTC_TIMEZONE, tokenParameters.TimezoneTerm.ToString()),
                new(Constants.LANGUAGE_ID, tokenParameters.LanguageId.ToString()),
            };

            var userClaims = claims.ToArray();

            var subject = new ClaimsIdentity(userClaims);
            var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = signinCredentials
            };

            token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
        }

        public async Task<string> RefreshUserToken(string login, TokenParameters tokenParameters)
        {
            var user = await GetBy(login);
            GetJwtTokenForUser(user, tokenParameters);
            return user.Token;
        }
    }
}
