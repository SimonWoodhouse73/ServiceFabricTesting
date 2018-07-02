using Callcredit.Domain.Repositories;

namespace Callcredit.Domain.Insolvencies.Repositories
{
    public interface IInsolvencyOrderHistoriesRepository<TModel, TEntity> : IDomainRepositoryAsync<TModel, TEntity>
        where TModel : class, new()
        where TEntity : class, new()
    {
    }
}
