// ***********************************************************************
// Assembly         : Api
// Author           : markco
// Created          : 03-01-2018
//
// Last Modified By : markco
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="ApiEventSource.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>ApiEventSource class</summary>
// ***********************************************************************
using System.Diagnostics.Tracing;

namespace Api.Logging
{
    /// <summary>
    /// Class ApiEventSource.
    /// </summary>
    /// <seealso cref="System.Diagnostics.Tracing.EventSource" />
    /// <seealso cref="Api.Logging.IApiEventSource" />
    public class ApiEventSource : EventSource, IApiEventSource
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static ApiEventSource instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiEventSource" /> class.
        /// </summary>
        /// <param name="eventSourceName">The name to apply to the event source. Must not be null.</param>
        private ApiEventSource(string eventSourceName)
            : base(eventSourceName)
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static ApiEventSource Instance => instance ?? (instance = new ApiEventSource("Callcredit.Mastered-Data.Callcredit.Insolvencies.Service.Api"));

        /// <summary>
        /// Executions the time.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="elapsedTime">The elapsed time.</param>
        [Event(1000, Level = EventLevel.Informational, Message = "{0} ::: Took {1} Milliseconds")]
        public void ExecutionTime(string operation, long elapsedTime)
        {
            if (IsEnabled())
            {
                WriteEvent(1000, operation, elapsedTime);
            }
        }

        /// <summary>
        /// Class Keywords.
        /// </summary>
        public static class Keywords
        {
            /// <summary>
            /// The service message
            /// </summary>
            public const EventKeywords ServiceMessage = (EventKeywords)0x1L;
        }
    }
}
