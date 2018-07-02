using System.Linq;
using Callcredit.FirstInFirstOutFiltering;
using Validation;

namespace Callcredit.Domain.Insolvencies.Filters.Dispute
{
    public class DisputeDisplayedFilter : IContextFilter<IDisputeFilterBase>
    {
        public IQueryable<IDisputeFilterBase> ApplyFilter(IQueryable<IDisputeFilterBase> unfinishedQuery)
        {
            Requires.NotNull(unfinishedQuery, nameof(unfinishedQuery));

            var displayed = true;

            var filterAppender = new FilterAppender();
            var updateQuery = filterAppender.AddToUnfinishedQuery(
                unfinishedQuery,
                disputes => disputes.Where(dispute => dispute.Displayed.Value == displayed));
            return updateQuery;
        }
    }
}
