using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Callcredit.AspNetCore.Middleware
{
    /// <summary>
    /// Microsoft do not use interfaces for implementing Middleware and instead leave it down to the user to implement the required method and mechanism, this leaves the possibility of human error.
    /// The following interface provides the requirements for implementing custom Middleware in the ASP.NET Core Pipeline.
    /// </summary>
    /// <remarks>
    /// This interface will/should never be used by any piece of the application other than the ASP.NET Core pipeline.
    /// This interface serves only as a ruleset to follow for implementing Middleware.
    /// Classes implementing this interface must contain a constructor accepting a parameter of <see cref="RequestDelegate"/> and any additional number of arguments which can be injected via the Services Collection.
    /// </remarks>
    public interface IMiddleware
    {
        /// <summary>
        /// This <see cref="System.Delegate"/> points to the next Middleware object in the pipeline, if the following middleware cannot handle the request or response then it should pass it to the next middleware in the pipeline.
        /// </summary>
        /// <remarks>This is injected in via the constructor.</remarks>
        RequestDelegate NextMiddlewareDelegate { get; }

        /// <summary>
        /// This is the execution point for any piece of Middleware, and is where operations should happen for the request/response.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> which contains the Request and Response objects.</param>
        /// <returns>Commonly nothing, to short-circuit the ASP.NET Core pipeline the <see cref="HttpResponse"/> object must be written to.</returns>
        Task Invoke(HttpContext context);
    }
}
