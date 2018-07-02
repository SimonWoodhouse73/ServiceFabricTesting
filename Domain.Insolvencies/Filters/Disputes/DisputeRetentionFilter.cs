using System;
using System.Linq;
using Callcredit.FirstInFirstOutFiltering;
using Callcredit.GDPR.Processing.Retention;
using Validation;

namespace Callcredit.Domain.Insolvencies.Filters.Dispute
{
    public class DisputeRetentionFilter : IContextFilter<IDisputeFilterBase>, IRetentionFilter
    {
        public int RetentionPeriod { get; set; }

        public IQueryable<IDisputeFilterBase> ApplyFilter(IQueryable<IDisputeFilterBase> unfinishedQuery)
        {
            Requires.NotNull(unfinishedQuery, nameof(unfinishedQuery));

            var filterAppender = new FilterAppender();
            var oldestDateAllowed = DateTime.Now.AddYears(-RetentionPeriod).Date;
            var updateQuery = filterAppender.AddToUnfinishedQuery(
                unfinishedQuery,
                disputes => disputes.Where(dispute => dispute.DateRaised.Value > oldestDateAllowed));
            return updateQuery;
                     
        }

        public IContextFilter<IDisputeFilterBase> OnlyDataWithinRetentionPeriod(int years)
        {
            Requires.Range(years > 0, nameof(years), "Years period must be greater or equals zero.");

            RetentionPeriod = years;
            return this;
        }
    }
}
