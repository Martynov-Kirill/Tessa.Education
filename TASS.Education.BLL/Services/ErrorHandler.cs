using AutoMapper;
using Tessa.Education.BLL.Models;
using Tessa.Education.Entites.Entities;

namespace Tessa.Education.BLL.Services
{
    public class ErrorHandler : Exception
    {
        private readonly IMapper _mapper;
        public readonly Error ErrorInfo;

        public ErrorHandler(string? mnemocode)
        {
            ErrorInfo = ErrorBuilder.GetError(mnemocode).Result;
        }
        public ErrorHandler(IMapper mapper, ErrorModel errorModel)
        {
            _mapper = mapper ?? throw new NotImplementedException();
            ErrorInfo = _mapper.Map<Error>(errorModel);
        }
    }
}
