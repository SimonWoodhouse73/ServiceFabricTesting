using System;
using System.Diagnostics.Tracing;
using Validation;

namespace Callcredit.EntityFramework.Secrets.KeyVault
{
    public sealed class DatabaseContextEventSource :
        EventSource,
        IDatabaseContextEventSource
    {
        private const int DatabaseSqlExceptionEventId = 4000;

        private static readonly object InstanceLock = new object();
        private static DatabaseContextEventSource instance;

        public static void Initialise(string eventSourceName)
        {
            Requires.NotNullOrEmpty(eventSourceName, nameof(eventSourceName));

            lock (InstanceLock)
            {
                if (instance != null)
                {
                    if (IsDifferentEventSourceName(eventSourceName))
                    {
                        throw new InvalidOperationException($"Instance cannot be reinitialised with another name. Current name: {instance.Name}");
                    }

                    return;
                }

                instance = new DatabaseContextEventSource(eventSourceName);
            }
        }

        private static bool IsDifferentEventSourceName(string eventSourceName)
        {
            return !instance.Name.Equals(eventSourceName);
        }

        public static DatabaseContextEventSource GetInstance()
        {
            return instance ?? throw new InvalidOperationException("Instance is not initialised");
        }

        private DatabaseContextEventSource(string eventSourceName)
            : base(eventSourceName)
        {
        }

        public static class Keywords
        {
            public const EventKeywords ExecutionError = (EventKeywords)0x10000L;
        }

        [Event(
            DatabaseSqlExceptionEventId,
            Level = EventLevel.Error,
            Message = "{0} ::: {1} failed to execute operation to database.",
            Keywords = Keywords.ExecutionError)]
        public void SqlProblemOccured(string contextName, string exceptionMessage)
        {
            Requires.NotNullOrEmpty(contextName, nameof(contextName));
            Requires.NotNullOrEmpty(exceptionMessage, nameof(exceptionMessage));

            if (IsEnabled())
            {
                WriteEvent(DatabaseSqlExceptionEventId, contextName, exceptionMessage);
            }
        }
    }
}
