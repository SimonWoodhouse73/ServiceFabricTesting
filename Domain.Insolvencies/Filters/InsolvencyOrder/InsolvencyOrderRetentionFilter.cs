using System;
using System.Linq;
using Callcredit.FirstInFirstOutFiltering;
using Callcredit.GDPR.Processing.Retention;
using Validation;

namespace Callcredit.Domain.Insolvencies.Filters.InsolvencyOrder
{
    public class InsolvencyOrderRetentionFilter : IContextFilter<IInsolvencyOrderFilterBase>, IRetentionFilter
    {
        public int RetentionPeriod { get; set; }

        public IQueryable<IInsolvencyOrderFilterBase> ApplyFilter(IQueryable<IInsolvencyOrderFilterBase> unfinishedQuery)
        {
            Requires.NotNull(unfinishedQuery, nameof(unfinishedQuery));

            var filterAppender = new FilterAppender();
            var oldestDateAllowed = DateTime.Now.AddYears(-RetentionPeriod).Date;
            var updateQuery =
                filterAppender.AddToUnfinishedQuery(
                    unfinishedQuery,
                    insolvencyOrders => insolvencyOrders.Where(insolvency => insolvency.OrderDate > oldestDateAllowed));

            return updateQuery;                     
        }

        public IContextFilter<IInsolvencyOrderFilterBase> OnlyDataWithinRetentionPeriod(int years)
        {
            Requires.Range(years > 0, nameof(years), "Years period must be greater or equals zero.");

            RetentionPeriod = years;
            return this;
        }
    }
}
