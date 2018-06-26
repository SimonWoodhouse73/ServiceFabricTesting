using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Callcredit.RESTful.Services.ServiceFabric;
using Microsoft.Diagnostics.EventFlow.ServiceFabric;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Api
{
    /// <summary>
    /// Class Program.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        /// <summary>
        /// is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                using (var diagnosticPipeline = ServiceFabricDiagnosticPipelineFactory.CreatePipeline("Callcredit.Mastered-Data.Callcredit.Insolvencies.Service-Pipeline"))
                {
                    ServiceRuntime.RegisterServiceAsync("ApiType", context => new Service(context)).GetAwaiter().GetResult();

                    PlatformEventSource.Initialise("Callcredit.Mastered-Data.Callcredit.Insolvencies.Service.Platform");
                    PlatformEventSource.GetInstance().ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(Service).Name);

                    Thread.Sleep(Timeout.Infinite);
                }
            }
            catch (Exception e)
            {
                PlatformEventSource.GetInstance().ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
