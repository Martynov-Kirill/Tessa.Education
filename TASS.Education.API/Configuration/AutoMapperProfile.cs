using AutoMapper;

using Tessa.Education.API.Models;
using Tessa.Education.API.Models.Dto;
using Tessa.Education.BLL.Models;
using Tessa.Education.Entites.Entities;

namespace Tessa.Education.API.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // DTOs -> to -> models 
            CreateMap<StudentDto, Student>();
            CreateMap<UserDto, User>();
            CreateMap<AuthenticateModel, User>();
            CreateMap<UserActivityDto, UserActivity>();

            // Models -> to -> DTOs 
            CreateMap<User, UserDto>();
            CreateMap<Student, StudentDto>();
            CreateMap<User, AuthenticateModel>();

            // Models -> to -> Models
            CreateMap<Error, ErrorModel>();
            CreateMap<ErrorModel, Error>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}