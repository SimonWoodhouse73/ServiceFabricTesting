using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Validation;

namespace Callcredit.AspNetCore.ProblemJson
{
    /// <summary>
    /// Represents the result of a problem action.
    /// </summary>
    public class ProblemActionResult : IActionResult
    {
        private readonly Problem problem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemActionResult"/> class.
        /// </summary>
        /// <param name="problem">Parameter representing a Problem class</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="problem"/> is null.</exception>
        public ProblemActionResult(Problem problem)
        {
            Requires.NotNull(problem, nameof(problem));
            this.problem = problem;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemActionResult"/> class containing the specified <seealso cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="httpStatusCode">Provided http status code</param>
        public ProblemActionResult(HttpStatusCode httpStatusCode)
            : this(new Problem(httpStatusCode))
        {
        }

        /// <summary>
        /// Creates a new <see cref="ObjectResult"/> and sets the content as the <seealso cref="Problem"/>.
        /// </summary>
        /// <param name="context">The context of the action whose result is being executed.</param>
        /// <returns><see cref="Task"/> representing the object result execution.</returns>
        public Task ExecuteResultAsync(ActionContext context)
        {
            Requires.NotNull(context, nameof(context));

            var objectResult = new ObjectResult(problem)
            {
                StatusCode = (int)problem.Status,
                ContentTypes = new MediaTypeCollection { Problem.ContentType },
                DeclaredType = typeof(Problem)
            };

            return objectResult.ExecuteResultAsync(context);
        }
    }
}
