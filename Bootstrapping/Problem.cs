using System.Diagnostics.CodeAnalysis;
using Callcredit.AspNetCore.ProblemJson;
using Callcredit.RESTful.Standards.Problems;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class Problems.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Problems
    {
        /// <summary>
        /// Adds the ProblemJson provider to the Insolvency Service.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        public static void AddProblemProviders(this IServiceCollection services)
        {
            services.AddScoped<IProblemProvider, ProblemProvider>();
            services.AddScoped<IProblemMapper, ProblemMapper>();
        }
    }
}
