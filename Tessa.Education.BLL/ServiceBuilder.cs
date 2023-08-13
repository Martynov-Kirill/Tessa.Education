using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Tessa.Education.BLL.Services;
using Tessa.Education.BLL.Services.Interfaces;

namespace Tessa.Education.BLL
{
    public class ServiceBuilder
    {
        public static void BuildServices(IServiceCollection services)
        {
            services.AddScoped<ILog, LoggerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISubjectService, SubjectService>();

            DAL.ServiceBuilder.BuildServices(services);
        }
    }
}
