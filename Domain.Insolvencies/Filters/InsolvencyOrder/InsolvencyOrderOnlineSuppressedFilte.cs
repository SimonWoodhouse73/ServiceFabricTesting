using System.Linq;
using Callcredit.FirstInFirstOutFiltering;
using Validation;

namespace Callcredit.Domain.Insolvencies.Filters.InsolvencyOrder
{
    public class InsolvencyOrderOnlineSuppressedFilter : IContextFilter<IInsolvencyOrderFilterBase>
    {
        public IQueryable<IInsolvencyOrderFilterBase> ApplyFilter(IQueryable<IInsolvencyOrderFilterBase> unfinishedQuery)
        {
            Requires.NotNull(unfinishedQuery, nameof(unfinishedQuery));

            var filterAppender = new FilterAppender();
            var onlineSuppressed = false;
            var updateQuery = 
                filterAppender.AddToUnfinishedQuery(
                    unfinishedQuery,
                    insolvencyOrders => insolvencyOrders.Where(insolvency => insolvency.OnlineSuppressed == onlineSuppressed));
            return updateQuery;
        }
    }
}
