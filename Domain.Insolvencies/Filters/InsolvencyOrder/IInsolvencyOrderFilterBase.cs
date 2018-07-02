using System;

namespace Callcredit.Domain.Insolvencies.Filters.InsolvencyOrder
{
    public interface IInsolvencyOrderFilterBase
    {
        bool OnlineSuppressed { get; set; }

        DateTime? OrderDate { get; set; }
    }
}
