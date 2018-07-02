using System;
using System.Linq;
using Callcredit.Domain.Repositories;
using Callcredit.FirstInFirstOutFiltering;
using Validation;

namespace Callcredit.Domain.Insolvencies.Filters.InsolvencyOrder
{
    public class InsolvencyOrderOrderDateRangeFilter : IContextFilter<IInsolvencyOrderFilterBase>
    {
        private readonly DateTime operationDate;
        private int cutOffPeriod;

        public InsolvencyOrderOrderDateRangeFilter(IOperationDateProvider operationDateProvider)
        {
            Requires.NotNull(operationDateProvider, nameof(operationDateProvider));

            this.operationDate = operationDateProvider.GetOperationDate();
        }

        public IQueryable<IInsolvencyOrderFilterBase> ApplyFilter(IQueryable<IInsolvencyOrderFilterBase> unfinishedQuery)
        {
            Requires.NotNull(unfinishedQuery, nameof(unfinishedQuery));

            var filterAppender = new FilterAppender();
            var cutOffDate = this.operationDate.AddYears(-cutOffPeriod).Date;
            var updateQuery =
                filterAppender.AddToUnfinishedQuery(
                    unfinishedQuery,
                    insolvencyOrders =>
                        insolvencyOrders
                        .Where(insolvency =>
                            insolvency.OrderDate > cutOffDate
                            && insolvency.OrderDate <= this.operationDate.Date));
            return updateQuery;
        }

        public IContextFilter<IInsolvencyOrderFilterBase> OnlyDataWithinCutoffPeriod(int cutOff)
        {
            Requires.Range(cutOff > 0, nameof(cutOff), "Cut off period must be greater than zero.");

            cutOffPeriod = cutOff;
            return this;
        }
    }
}
