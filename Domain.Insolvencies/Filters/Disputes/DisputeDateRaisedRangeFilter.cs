using System;
using System.Linq;
using Callcredit.Domain.Repositories;
using Callcredit.FirstInFirstOutFiltering;
using Validation;

namespace Callcredit.Domain.Insolvencies.Filters.Dispute
{
    public class DisputeDateRaisedRangeFilter : IContextFilter<IDisputeFilterBase>
    {
        private readonly DateTime operationDate;
        private int cutOffPeriod;

        public DisputeDateRaisedRangeFilter(IOperationDateProvider operationDateProvider)
        {
            Requires.NotNull(operationDateProvider, nameof(operationDateProvider));

            this.operationDate = operationDateProvider.GetOperationDate();
        }

        public IQueryable<IDisputeFilterBase> ApplyFilter(IQueryable<IDisputeFilterBase> unfinishedQuery)
        {
            Requires.NotNull(unfinishedQuery, nameof(unfinishedQuery));

            var filterAppender = new FilterAppender();
            var cutOffDate = operationDate.AddYears(-cutOffPeriod).Date;
            var updateQuery = filterAppender.AddToUnfinishedQuery(
                unfinishedQuery,
                disputes =>
                    disputes
                    .Where(
                        dispute =>
                        dispute.DateRaised.Value > cutOffDate
                        && dispute.DateRaised.Value <= operationDate.Date));
            return updateQuery;
        }

        public IContextFilter<IDisputeFilterBase> OnlyDataWithinCutoffPeriod(int cutOff)
        {
            Requires.Range(cutOff > 0, nameof(cutOff), "Cut off period must be greater than zero.");

            cutOffPeriod = cutOff;
            return this;
        }
    }
}
