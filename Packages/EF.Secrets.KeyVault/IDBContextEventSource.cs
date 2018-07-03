namespace Callcredit.EntityFramework.Secrets.KeyVault
{
    public interface IDatabaseContextEventSource
    {
        void SqlProblemOccured(string contextName, string exceptionMessage);
    }
}
