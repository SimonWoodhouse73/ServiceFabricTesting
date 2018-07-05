using System.Net;

namespace Callcredit.AspNetCore.ProblemJson
{
    /// <summary>
    /// Provides method for obtaining Http Status phrases. Similar implementation to System.Net.HttpStatusDescription which we cannot use as it's an internal class
    /// </summary>
    public static class HttpStatusDescription
    {
        /// <summary>
        /// Get the typical message phrase for the given HTTP Status Code.
        /// Note that not all HTTP Status Codes are defined in the HttpStatusCode enum (e.g. 418 I'm a teapot).
        /// </summary>
        /// <param name="code">A valid HttpStatusCode</param>
        /// <returns>A typical message phrase of a HTTP status code or null if message is not defined</returns>
        public static string Get(HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.Continue: return "Continue";
                case HttpStatusCode.SwitchingProtocols: return "Switching Protocols";

                case HttpStatusCode.OK: return "OK";
                case HttpStatusCode.Created: return "Created";
                case HttpStatusCode.Accepted: return "Accepted";
                case HttpStatusCode.NonAuthoritativeInformation: return "Non-Authoritative Information";
                case HttpStatusCode.NoContent: return "No Content";
                case HttpStatusCode.ResetContent: return "Reset Content";
                case HttpStatusCode.PartialContent: return "Partial Content";

                case HttpStatusCode.MultipleChoices: return "Multiple Choices";
                case HttpStatusCode.MovedPermanently: return "Moved Permanently";
                case HttpStatusCode.Found: return "Found";
                case HttpStatusCode.SeeOther: return "See Other";
                case HttpStatusCode.NotModified: return "Not Modified";
                case HttpStatusCode.UseProxy: return "Use Proxy";
                case HttpStatusCode.Unused: return "Switch Proxy";
                case HttpStatusCode.TemporaryRedirect: return "Temporary Redirect";

                case HttpStatusCode.BadRequest: return "Bad Request";
                case HttpStatusCode.Unauthorized: return "Unauthorized";
                case HttpStatusCode.PaymentRequired: return "Payment Required";
                case HttpStatusCode.Forbidden: return "Forbidden";
                case HttpStatusCode.NotFound: return "Resource Not Found";
                case HttpStatusCode.MethodNotAllowed: return "Method Not Allowed";
                case HttpStatusCode.NotAcceptable: return "Not Acceptable";
                case HttpStatusCode.ProxyAuthenticationRequired: return "Proxy Authentication Required";
                case HttpStatusCode.RequestTimeout: return "Request Timeout";
                case HttpStatusCode.Conflict: return "Conflict";
                case HttpStatusCode.Gone: return "Gone";
                case HttpStatusCode.LengthRequired: return "Length Required";
                case HttpStatusCode.PreconditionFailed: return "Precondition Failed";
                case HttpStatusCode.RequestEntityTooLarge: return "Request Entity Too Large";
                case HttpStatusCode.RequestUriTooLong: return "Request-Uri Too Long";
                case HttpStatusCode.UnsupportedMediaType: return "Unsupported Media Type";
                case HttpStatusCode.RequestedRangeNotSatisfiable: return "Requested Range Not Satisfiable";
                case HttpStatusCode.ExpectationFailed: return "Expectation Failed";
                case HttpStatusCode.UpgradeRequired: return "Upgrade Required";

                case HttpStatusCode.InternalServerError: return "Internal Server Error";
                case HttpStatusCode.NotImplemented: return "Not Implemented";
                case HttpStatusCode.BadGateway: return "Bad Gateway";
                case HttpStatusCode.ServiceUnavailable: return "Service Unavailable";
                case HttpStatusCode.GatewayTimeout: return "Gateway Timeout";
                case HttpStatusCode.HttpVersionNotSupported: return "Http Version Not Supported";
                default: return null;
            }
        }
    }
}
