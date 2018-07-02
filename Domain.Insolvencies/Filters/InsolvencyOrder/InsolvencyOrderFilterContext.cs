using System.Linq;
using Callcredit.Domain.Repositories;
using Callcredit.Domain.Repositories.GDPR;
using Callcredit.FirstInFirstOutFiltering;
using Microsoft.Extensions.Options;
using Validation;

namespace Callcredit.Domain.Insolvencies.Filters.InsolvencyOrder
{
    public class InsolvencyOrderFilterContext : IFilteredBaseData<IInsolvencyOrderFilterBase>
    {
        private readonly RetentionOptions retentionOptions;
        private readonly IOperationDateProvider operationDateProvider;

        public InsolvencyOrderFilterContext(IOptions<RetentionOptions> retentionOptions, IOperationDateProvider operationDateProvider)
        {
            Requires.NotNull(retentionOptions, nameof(retentionOptions));
            Requires.NotNull(operationDateProvider, nameof(operationDateProvider));

            this.operationDateProvider = operationDateProvider;
            this.retentionOptions = retentionOptions.Value;
        }

        public IQueryable<IInsolvencyOrderFilterBase> FilteredContext(IQueryable<IInsolvencyOrderFilterBase> unfilteredData)
        {
            Requires.NotNull(unfilteredData, nameof(unfilteredData));

            return
                new FirstInFirstOutFilterSequence<IInsolvencyOrderFilterBase>(unfilteredData)
                .Add(
                    new AlwaysExecute<IInsolvencyOrderFilterBase>()
                    .FilterUsing(
                        new InsolvencyOrderRetentionFilter()
                        .OnlyDataWithinRetentionPeriod(retentionOptions.RetentionPeriod)))
                .Add(
                    new AlwaysExecute<IInsolvencyOrderFilterBase>()
                    .FilterUsing(
                        new InsolvencyOrderOrderDateRangeFilter(operationDateProvider)
                        .OnlyDataWithinCutoffPeriod(retentionOptions.CutOffPeriod)))
                .Add(
                    new AlwaysExecute<IInsolvencyOrderFilterBase>()
                    .FilterUsing(
                        new InsolvencyOrderOnlineSuppressedFilter()))
                .ApplyFilters();
        }
    }
}
