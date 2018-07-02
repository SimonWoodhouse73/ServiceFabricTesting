using System;

namespace Callcredit.Domain.Insolvencies.Filters.Dispute
{
    public interface IDisputeFilterBase
    {
        bool? Displayed { get; set; }

        DateTime? DateRaised { get; set; }
    }
}
