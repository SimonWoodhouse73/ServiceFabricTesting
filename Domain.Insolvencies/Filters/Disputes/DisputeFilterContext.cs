using System.Linq;
using Callcredit.Domain.Repositories;
using Callcredit.Domain.Repositories.GDPR;
using Callcredit.FirstInFirstOutFiltering;
using Callcredit.RESTful.DataAssets;
using Microsoft.Extensions.Options;
using Validation;

namespace Callcredit.Domain.Insolvencies.Filters.Dispute
{
    public class DisputeFilterContext : IFilteredBaseData<IDisputeFilterBase>
    {
        private readonly RetentionOptions retentionOptions;
        private readonly IOperationDateProvider operationDateProvider;

        public DisputeFilterContext(IOptions<RetentionOptions> retentionOptions, IOperationDateProvider operationDateProvider)
        {
            Requires.NotNull(retentionOptions, nameof(retentionOptions));
            Requires.NotNull(operationDateProvider, nameof(operationDateProvider));

            this.operationDateProvider = operationDateProvider;
            this.retentionOptions = retentionOptions.Value;
        }

        public IQueryable<IDisputeFilterBase> FilteredContext(IQueryable<IDisputeFilterBase> unfilteredData)
        {
            Requires.NotNull(unfilteredData, nameof(unfilteredData));

            return
                new FirstInFirstOutFilterSequence<IDisputeFilterBase>(unfilteredData)
                .Add(
                    new AlwaysExecute<IDisputeFilterBase>()
                    .FilterUsing(
                        new DisputeRetentionFilter()
                        .OnlyDataWithinRetentionPeriod(retentionOptions.RetentionPeriod)))
                .Add(
                    new AlwaysExecute<IDisputeFilterBase>()
                    .FilterUsing(
                        new DisputeDateRaisedRangeFilter(operationDateProvider)
                        .OnlyDataWithinCutoffPeriod(retentionOptions.CutOffPeriod)))
                .Add(
                    new AlwaysExecute<IDisputeFilterBase>()
                    .FilterUsing(new DisputeDisplayedFilter()))
                .ApplyFilters();
        }
    }
}
