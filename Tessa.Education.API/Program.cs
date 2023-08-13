using Tessa.Education.DAL.DataBase;
using Tessa.Education.Entites.DataBase;

namespace Tessa.Education.API
{
    /// <summary>
    /// Start class Application
    /// </summary>
    public class Program
    {
        /// <summary>
        ///  Main Start point of Application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //TODO : Change Connection string in appsettings.json DataBaseSettings.ConnectionString
            CreateHostBuilder(args).Build().MigrateDatabase().Run();
        }

        /// <summary>
        /// Application builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}