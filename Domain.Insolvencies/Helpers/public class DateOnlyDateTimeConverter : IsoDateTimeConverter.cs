using Newtonsoft.Json.Converters;

namespace Callcredit.Domain.Insolvencies.Helpers
{
    public class DateOnlyDateTimeConverter : IsoDateTimeConverter
    {
        public DateOnlyDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
