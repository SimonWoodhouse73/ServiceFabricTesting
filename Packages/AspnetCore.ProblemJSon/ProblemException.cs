using System;
using Validation;

namespace Callcredit.AspNetCore.ProblemJson
{
    /// <summary>
    /// Problem Exception which encapsulates a problem detail object
    /// </summary>
    [Serializable]
    public class ProblemException : Exception
    {
        /// <summary>
        /// The <see cref="Problem"/> being raised.
        /// </summary>
        public Problem ProblemDetails { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemException"/> class.
        /// </summary>
        /// <param name="problem">Represents the <see cref="Problem"/> used for creating the <see cref="ProblemException"/>.</param>
        public ProblemException(Problem problem)
        {
            Requires.NotNull(problem, nameof(problem));
            ProblemDetails = problem;
        }
    }
}
