using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BootStrapping
{
    /// <summary>
    /// Class ResponseCompression.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ResponseCompression
    {
        /// <summary>
        /// Adds the gzip compression service.
        /// </summary>
        /// <param name="services">The services collection used to configure the Insolvency Service.</param>
        public static void AddGzipCompression(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(
                options => options.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(
                options => options.MimeTypes = new[]
                {
                    "application/hal+json",
                    "application/problem+json"
                });
        }
    }
}
