using System;

namespace Callcredit.AspNetCore.ProblemJson
{
    /// <summary>
    /// <see cref="IProblemProvider"/> interface that defines methods to be used when providing problems.
    /// </summary>
    public interface IProblemProvider
    {
        /// <summary>
        /// Provides a problem based on the exception passed in.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> used to provide the problem.</param>
        /// <returns><see cref="Problem"/></returns>
        Problem ProvideProblem(Exception exception);
    }
}
