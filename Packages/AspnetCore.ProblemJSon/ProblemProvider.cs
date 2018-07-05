using System;
using Validation;

namespace Callcredit.AspNetCore.ProblemJson
{
    /// <summary>
    /// Class used to provide problems based on certain exceptions.
    /// </summary>
    public class ProblemProvider : IProblemProvider
    {
        private readonly IProblemMapper problemMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemProvider"/> class.
        /// </summary>
        /// <param name="problemMapper">Provides a method to retrieve the currently mapped exceptions</param>
        public ProblemProvider(IProblemMapper problemMapper)
        {
            Requires.NotNull(problemMapper, nameof(problemMapper));
            this.problemMapper = problemMapper;
        }

        /// <summary>
        /// Provides a <see cref="Problem"/> based on the <see cref="Exception"/> passed in.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> used to create the <see cref="Problem"/>.</param>
        /// <returns><see cref="Problem"/></returns>
        public Problem ProvideProblem(Exception exception)
        {
            Requires.NotNull(exception, nameof(exception));

            var mappings = problemMapper.GetMappedExceptions(exception);

            var exceptionType = exception.GetType();
            if (!mappings.ContainsKey(exceptionType))
            {
                return null;
            }

            var mapping = mappings[exceptionType];
            var problem = new Problem(mapping.StatusCode)
            {
                Title = mapping.Title,
                Detail = mapping.Detail
            };

            return problem;
        }
    }
}
