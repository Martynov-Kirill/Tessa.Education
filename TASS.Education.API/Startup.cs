using AutoMapper;
using System.Text;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

using Tessa.Education.API.Configuration;
using Tessa.Education.API.Middleware;
using Tessa.Education.API.Models;
using Tessa.Education.BLL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Tessa.Education.BLL;
using Tessa.Education.BLL.Models;
using Tessa.Education.DAL.DataBase;

namespace Tessa.Education.API
{
    public class Startup
    {
        private AppSettings? _appSettings = default;
        private DataBaseSettings? _dbSettings = default;

        public Startup(IHostEnvironment env)
        {
            Configuration =
                ConfigurationHelper.GetIConfigurationRoot(env.EnvironmentName, env.ContentRootPath);

            HostEnvironment = env;
        }
        public IConfiguration Configuration { get; }

        public IHostEnvironment HostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Enable Cors
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            
            // configure strongly typed settings objects
            _appSettings = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

            //Add IMapper profile configuration to DI
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperProfile()));
            services.AddSingleton(mapperConfig.CreateMapper());

            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            services.AddSession();
            services.AddOptions();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.Configure<DataBaseSettings>(Configuration.GetSection(nameof(DataBaseSettings)));
            var databaseConfiguration = Configuration.GetSection(nameof(DataBaseSettings)).Get<DataBaseSettings>();
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(databaseConfiguration?.ConnectionString,
                    x => x.MigrationsAssembly(databaseConfiguration?.MigrationAssemblyName))
                , contextLifetime: ServiceLifetime.Transient);

            // -- API Rate Limit Configuration DDos attack security --
            services.AddMemoryCache();
            services.AddInMemoryRateLimiting();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            //Add Services here
            ServiceBuilder.BuildServices(services);

            //API versioning
            services.AddApiVersioning(
                o =>
                {
                    //o.Conventions.Controller<UserController>().HasApiVersion(1, 0);
                    o.ReportApiVersions = true;
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                    o.ApiVersionReader = new UrlSegmentApiVersionReader();
                }
            );

            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    // can also be used to control the format of the API version in route accounting-service
                    options.SubstituteApiVersionInUrl = true;
                });

            // -- Swagger configuration -- 
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigurationSwaggerOptions>();

            services.AddSwaggerGen(c =>
            {
                c.UseAllOfToExtendReferenceSchemas();

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.UseInlineDefinitionsForEnums();
                c.DescribeAllParametersInCamelCase();
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
                AddSwaggerXml(c);
            });

            services.ConfigureSwaggerGen(options =>
            {
                var basePath = AppContext.BaseDirectory;
                var assemblyName = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name;
                var fileName = Path.GetFileName(assemblyName + ".xml");
                options.IncludeXmlComments(Path.Combine(basePath, fileName),
                    includeControllerXmlComments: true);
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILog logger,
            IServiceProvider serviceProvider, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomMiddleware(_appSettings);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            if (_appSettings is null || !_appSettings.Swagger.Enabled)
                return;

            app.UseSwagger(delegate (SwaggerOptions c)
            {
                c.RouteTemplate = $"/{_appSettings.ServiceName}/swagger/{{documentName}}/swagger.json";
            }).UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/{_appSettings!.ServiceName}/swagger/{description.GroupName}/swagger.json",
                        $"{_appSettings.ServiceTitle} {description.GroupName}");
                    options.RoutePrefix = $"{_appSettings.ServiceName}/swagger";
                }
            });
        }
        private static void AddSwaggerXml(SwaggerGenOptions c)
        {
            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
            foreach (var xmlFile in xmlFiles)
            {
                c.IncludeXmlComments(xmlFile);
            }
        }
    }
}
