using System.Net;

namespace Callcredit.AspNetCore.ProblemJson
{
    /// <summary>
    /// <see cref="IProblemMapper"/> interface that defines the properties for mapping exceptions to problems.
    /// </summary>
    public interface IProblemMapping
    {
        /// <summary>
        /// Gets status code representing the problem.
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the title of the problem.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets detailed information about the problem.
        /// </summary>
        string Detail { get; }
    }
}
