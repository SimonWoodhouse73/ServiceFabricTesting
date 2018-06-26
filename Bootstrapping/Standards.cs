using System.Diagnostics.CodeAnalysis;
using Callcredit.RESTful.Standards;
using Callcredit.RESTful.Standards.Mandatory;
using Callcredit.RESTful.Standards.Permitted;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class Standards.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Standards
    {
        /// <summary>
        /// Adds the standards validation.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        public static void AddStandardsValidation(this IServiceCollection services)
        {
            services.AddScoped<IHeaderValidatorExecutor, HeaderValidatorExecutor>();
            services.AddScoped<IMandatoryResponseHeaderProvider, MandatoryResponseHeaderProvider>();
            services.AddScoped<IHeaderValidatorProvider, HeaderValidatorProvider>();
        }
    }
}
