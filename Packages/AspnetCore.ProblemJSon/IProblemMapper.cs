using System;
using System.Collections.Generic;

namespace Callcredit.AspNetCore.ProblemJson
{
    /// <summary>
    /// <see cref="IProblemMapper"/> interface that defines methods to be used when retrieving mapped exceptions.
    /// </summary>
    public interface IProblemMapper
    {
        /// <summary>
        /// Retrieves the currently mapped exceptions.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to retreive mappings for.</param>
        /// <returns>A dictionary of <see cref="Type"/> and <see cref="IProblemMapping"/></returns>
        IDictionary<Type, IProblemMapping> GetMappedExceptions(Exception exception);
    }
}
