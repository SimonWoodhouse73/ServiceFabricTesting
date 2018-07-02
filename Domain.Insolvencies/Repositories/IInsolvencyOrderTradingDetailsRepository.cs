using Callcredit.Domain.Repositories;

namespace Callcredit.Domain.Insolvencies.Repositories
{
    public interface IInsolvencyOrderTradingDetailsRepository<TModel, TEntity> : IDomainRepositoryAsync<TModel, TEntity>
        where TModel : class, new()
        where TEntity : class, new()
    {
    }
}
