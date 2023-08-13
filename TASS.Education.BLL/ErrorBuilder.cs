using AutoMapper;
using Tessa.Education.BLL.Models;
using Tessa.Education.BLL.Services.Interfaces;
using Tessa.Education.Entites.Entities;
using Tessa.Education.Entites.Models;

namespace Tessa.Education.BLL
{
    public static class ErrorBuilder 
    {
        private static IErrorService? _errorService;
        private static IMapper? _mapper = default;
        public static void InitServices(IErrorService errorService, IMapper mapper)
        {
            _errorService = errorService;
            _mapper = mapper;
        }

        private static Task SendCreateLogRequest(ErrorModel error, Exception e)
        {
            _errorService.Create(new()
            {
                Description = error.Description,
                MnemonicCode = error.MnemonicCode,
                Name = error.Name,
                Params = default,
                ErrorDetails = new ErrorDetails()
                {
                    Details = e.Message,
                    ErrorType = e.GetType().Name,
                    StackTrace = e.StackTrace
                }
            }).ConfigureAwait(false);
            return Task.CompletedTask;
        }
        public static async Task LogError(ErrorModel error, Exception e)
        {
            await SendCreateLogRequest(error, e);
        }

        public static async Task<Error> GetError(string? mnemonicCode)
        {
            try
            {
                var model = await _errorService.GetByMnemonicCode(mnemonicCode);

                return model;
            }
            catch
            {
                var model = await _errorService.GetByMnemonicCode("GEN_ERR");

                return model;
            }
        }
      
        public static async Task<ErrorModel> ShowError(ErrorModel error, Exception e = default, bool save = false)
        {
            try
            {
                if (save)
                    await LogError(error, e);
                return error;
            }
            catch
            {
                var result = _mapper.Map<ErrorModel>(await GetError("GEN_ERR"));
                if (save)
                    await LogError(result, e);
                return result;
            }
        }
    }
}
