namespace Tessa.Education.BLL.Models
{
    /// <summary>
    /// Application base configuration like swagger, license, secret and etc.
    /// </summary>
    public class AppSettings
    {
        public string Secret { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string ServiceTitle { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public Swagger Swagger { get; set; }
        public ApiSettings? API { get; set; }
    }
    public class Swagger
    {
        public bool Enabled { get; set; }
    }
    public class ApiSettings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int DefaultVersionMinor { get; set; }

        public int DefaultVersionMajor { get; set; }

        public ApiContact Contact { get; set; }

        public string TermsOfServiceUrl { get; set; }

        public ApiLicense License { get; set; }
    }
    public class ApiContact
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Url { get; set; }
    }
    public class ApiLicense
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
