namespace Tessa.Education.API.Configuration
{
    /// <summary>
    /// Main Appsetting configuration constructors
    /// </summary>
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(!string.IsNullOrEmpty(outputPath) ? outputPath : Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return configurationBuilder.Build();
        }

        public static IConfigurationRoot GetIConfigurationRoot(string environment, string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(!string.IsNullOrEmpty(outputPath) ? outputPath : Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static T? GetApplicationConfiguration<T>(string outputPath, IConfigurationRoot iConfig)
        {
            var configuration = default(T);

            iConfig?
                .GetSection(typeof(T).Name)?
                .Bind(configuration);

            return configuration;
        }

        public static IConfigurationSection? GetConfigurationSection<T>(IConfigurationRoot iConfig)
        {
            return iConfig?.GetSection(typeof(T).Name);
        }
    }
}
