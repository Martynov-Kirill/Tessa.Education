using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tessa.Education.API.Models;
using Tessa.Education.BLL.Services;
using Tessa.Education.BLL.Services.Interfaces;
using Tessa.Education.Entites.Entities;
using Tessa.Education.Entites.Models;
using Tessa.Education.Entites.Models.Errors.Enums;

namespace Tessa.Education.API.Controllers.V1
{
    public class UserController : AbstractController
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Авторизация пользователя по логину и паролю
        /// </summary>
        /// <param name="model">Модель для авторизации по логину и паролю</param>
        /// <param name="timezoneTerm">Временная зона пользователя</param>
        /// <param name="languageId">Язык интерфейса пользователя (по дефолту ru)</param>
        /// <returns></returns>
        /// <exception cref="Error"></exception>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model, [FromHeader] int timezoneTerm, [FromHeader] int languageId = 1)
        {
            if (model is null)
                throw new ErrorHandler(Enum.GetName(ErrorType.DATA_ERR));

            var user = await _service.Authenticate(_mapper.Map<User>(model));

            _service.GetJwtTokenForUser(user, new TokenParameters()
            {
                TimezoneTerm = timezoneTerm,
                login = null,
                LanguageId = languageId
            });

            user.Activity.LastSeenDate = DateTime.Now.ToUniversalTime();
            await _service.Update(user.Id, user);

            return Ok(user);
        }

        [HttpGet("refreshToken")]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromHeader] int languageId = 1)
        {
            var tokenParameters = new TokenParameters()
            {
                LanguageId = languageId,
                login = User.Claims.FirstOrDefault(c => c.Type == Constants.LOGIN)!.Value
                    .ToString(),
                TimezoneTerm =
                    Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == Constants.UTC_TIMEZONE)!.Value)
            };

            return Ok(await _service.RefreshUserToken(User.Identity.Name, tokenParameters));
        }
    }
}
