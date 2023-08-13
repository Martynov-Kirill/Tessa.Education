using Microsoft.Extensions.DependencyInjection;
using Tessa.Education.DAL.DataBase;
using Tessa.Education.Entites.DataBase;
using Tessa.Education.Entites.DataBase.Interfaces;

namespace Tessa.Education.DAL
{
    public class ServiceBuilder
    {
        public static void BuildServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
