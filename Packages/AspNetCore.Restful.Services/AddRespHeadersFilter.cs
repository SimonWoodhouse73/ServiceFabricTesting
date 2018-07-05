using Callcredit.RESTful.Standards.Mandatory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services
{
    public class AddMandatoryResponseHeadersFilter : ActionFilterAttribute
    {
        private readonly IMandatoryResponseHeaderProvider mandatoryResponseHeaderProvider;

        public AddMandatoryResponseHeadersFilter(IMandatoryResponseHeaderProvider mandatoryResponseHeaderProvider)
        {
            Requires.NotNull(mandatoryResponseHeaderProvider, nameof(mandatoryResponseHeaderProvider));

            this.mandatoryResponseHeaderProvider = mandatoryResponseHeaderProvider;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Requires.NotNull(context, nameof(context));

            ApplyHeaders(context.HttpContext.Response);
        }

        private void ApplyHeaders(HttpResponse response)
        {
            var headers = response.Headers;
            var headerSetters = mandatoryResponseHeaderProvider.GetSetters();

            foreach (var mandatoryResponseHeader in headerSetters)
            {
                if (!headers.ContainsKey(mandatoryResponseHeader.HeaderKey))
                {
                    var headerKey = mandatoryResponseHeader.HeaderKey;
                    var headerValue = mandatoryResponseHeader.GetHeaderValue();

                    response.Headers.Add(headerKey, headerValue);
                }
            }
        }
    }
}
