using System;
using System.Diagnostics.CodeAnalysis;
using System.Fabric;
using System.Net;
using Api.BootStrapping;
using Callcredit.AspNetCore.Common;
using Callcredit.AspNetCore.ProblemJson;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api
{
    /// <summary>
    /// Class Startup.
    /// </summary>
    /// <seealso cref="Callcredit.AspNetCore.Common.ITestableStartup" />
    [ExcludeFromCodeCoverage]
    public class Startup : ITestableStartup
    {
        /// <summary>
        /// The service name
        /// </summary>
        private readonly string serviceName;
        /// <summary>
        /// The env
        /// </summary>
        private IHostingEnvironment env;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="codePackageActivationContext">The code package activation context.</param>
        /// <param name="serviceContextServiceName">Name of the service context service.</param>
        public Startup(
            IHostingEnvironment env,
            ICodePackageActivationContext codePackageActivationContext,
            Uri serviceContextServiceName)
        {
            this.env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            CodePackageActivationContext = codePackageActivationContext;
            serviceName = serviceContextServiceName.AbsolutePath;

            // Enable TLS 1.2
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Gets or sets the code package activation context.
        /// </summary>
        /// <value>The code package activation context.</value>
        public ICodePackageActivationContext CodePackageActivationContext { get; set; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddEventSourceLogger();
            loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);

            if (!env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                });
            }

            app.UseMiddleware<ProblemExceptionMiddleware>();
            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseMvc();
        }

        // method gets called by the runtime. Use method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsights(Configuration);
            services.AddKeyVaultComponents(Configuration.GetSection(serviceName));
            services.AddDomainResources(Configuration);
            services.AddEventSources(Configuration);
            services.AddRestfulServices(Configuration);

            if (!env.IsDevelopment())
            {
                services.AddSwaggerGeneration();
            }

            services.AddHalFormatting();
            services.AddMvcComponents();
            services.AddProblemProviders();
            services.AddReaders();
            services.AddDataAccessCradle();
            services.AddGzipCompression();
            services.AddJwtHandlers();
            services.AddEndpointsConfiguration();
            services.AddStandardsValidation();
            services.AddHttps(CodePackageActivationContext, Configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = "uk/mastered-data/insolvencies";
                    options.Authority = Configuration.GetSection("JwtAuthorizationEndpoints").GetSection("Issuer").Value;
                    options.RequireHttpsMetadata = Convert.ToBoolean(Configuration.GetSection("RequireHttpsMetadata").GetSection("HttpsMetadataSetting").Value);
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Query", policy =>
                {
                    policy.Requirements.Add(new Callcredit.AspNetCore.Authorization.ScopeRequirement("uk/mastered-data/insolvencies.query"));
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddSingleton<IAuthorizationHandler, Callcredit.AspNetCore.Authorization.ScopeHandler>();
        }
    }
}
