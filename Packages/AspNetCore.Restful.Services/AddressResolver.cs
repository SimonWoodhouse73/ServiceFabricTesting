using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Callcredit.RESTful.Services;
using Callcredit.RESTful.Services.Hal;
using Callcredit.RESTful.Services.Readers;
using Halcyon.HAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Validation;

namespace Callcredit.AspNetCore.RESTful.Services
{
    public class AddressResolver :
        IAddressResolver
    {
        private const char UrlForwardslash = '/';
        private readonly IBaseQueryStringCreator queryStringCreator;
        private readonly IRequestReader requestReader;
        private readonly IQueryStringReader queryStringReader;
        private readonly IList<IBaseQueryStringWriter> baseQueryStringWriters;
        private readonly IActionDescriptorCollectionProvider actionDescriptorCollectionProvider;

        public AddressResolver(
            IQueryStringReader queryStringReader,
            IRequestReader requestReader,
            IBaseQueryStringCreator queryStringCreator,
            IList<IBaseQueryStringWriter> baseQueryStringWriters,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            Requires.NotNull(queryStringReader, nameof(queryStringReader));
            Requires.NotNull(requestReader, nameof(requestReader));
            Requires.NotNull(queryStringCreator, nameof(queryStringCreator));
            Requires.NotNull(baseQueryStringWriters, nameof(baseQueryStringWriters));
            Requires.NotNull(actionDescriptorCollectionProvider, nameof(actionDescriptorCollectionProvider));

            this.requestReader = requestReader;
            this.queryStringCreator = queryStringCreator;
            this.queryStringReader = queryStringReader;
            this.baseQueryStringWriters = baseQueryStringWriters;
            this.actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        /// <inheritdoc />
        public Link GetLinkToSelf()
        {
            return new Link(CommonLinks.Self, requestReader.GetRequestUri().PathAndQuery);
        }

        /// <inheritdoc />
        public Link GetLinkToEmbeddedCollection(string routeName, string linkName, string routeIdPlaceHolder, params string[] keys)
        {
            Requires.NotNullOrEmpty(routeName, nameof(routeName));
            Requires.NotNullOrEmpty(linkName, nameof(linkName));
            Requires.NotNullOrEmpty(routeIdPlaceHolder, nameof(routeIdPlaceHolder));
            Requires.NotNull(keys, nameof(keys));
            Requires.NullOrNotNullElements(keys, nameof(keys));

            var routeTemplate = GetActionRouteTemplate(routeName);

            var urlKeySegment = BuildKeySegment(keys);

            var linkText = routeTemplate.Replace(routeIdPlaceHolder, urlKeySegment);

            return new Link(linkName, linkText, replaceParameters: false);
        }

        /// <inheritdoc />
        public Link GetLinkToCollection(string routeName, string linkName, string routeIdPlaceHolder)
        {
            Requires.NotNullOrEmpty(routeName, nameof(routeName));
            Requires.NotNullOrEmpty(linkName, nameof(linkName));
            Requires.NotNullOrEmpty(routeIdPlaceHolder, nameof(routeIdPlaceHolder));

            var routeTemplate = GetActionRouteTemplate(routeName);

            var linkText = routeTemplate.ToLower().Replace(routeIdPlaceHolder.ToLower(), string.Empty);

            return new Link(linkName, linkText);
        }

        /// <inheritdoc />
        public Link GetLinkToEmbeddedItem(string routeName)
        {
            Requires.NotNullOrEmpty(routeName, nameof(routeName));

            var routeTemplate = GetActionRouteTemplate(routeName);

            return new Link(CommonLinks.Self, routeTemplate);
        }

        /// <inheritdoc />
        public Link GetPostLink(string routeName, string linkName)
        {
            Requires.NotNullOrEmpty(routeName, nameof(routeName));
            Requires.NotNullOrEmpty(linkName, nameof(linkName));

            var routeTemplate = GetActionRouteTemplate(routeName);

            return new Link(linkName, routeTemplate, method: HttpMethods.Post);
        }

        /// <inheritdoc />
        public Link GetParent(string routeIdPlaceHolder)
        {
            Requires.NotNullOrEmpty(routeIdPlaceHolder, nameof(routeIdPlaceHolder));

            var parentPath = requestReader.GetRequestUri().PathAndQuery.Replace(routeIdPlaceHolder, string.Empty);

            return new Link(CommonLinks.Parent, parentPath);
        }

        /// <inheritdoc />
        public Link GetLink(string routeName, string linkName)
        {
            Requires.NotNullOrEmpty(routeName, nameof(routeName));
            Requires.NotNullOrEmpty(linkName, nameof(linkName));

            var routeTemplate = GetActionRouteTemplate(routeName);

            return new Link(linkName, routeTemplate);
        }

        private AttributeRouteInfo GetActionRouteAttributeInfo(string routeName)
        {
            var route = actionDescriptorCollectionProvider.ActionDescriptors.Items.FirstOrDefault(actionDescriptor => string.Equals(actionDescriptor.AttributeRouteInfo.Name, routeName));
            if (route == null)
            {
                throw new ArgumentException(ExceptionMessages.RouteNotFound, nameof(routeName));
            }

            return route.AttributeRouteInfo;
        }

        private string GetActionRouteTemplate(string routeName)
        {
            var requestUri = requestReader.GetRequestUri();
            var requestQueryCollection = queryStringReader.GetQueryStrings(requestUri);

            var query = queryStringCreator.CreateBaseQueryString(requestQueryCollection, baseQueryStringWriters);
            var attributeRouteInfo = GetActionRouteAttributeInfo(routeName);

            var routeTemplateUri = new UriBuilder(attributeRouteInfo.Template) { Query = query };
            var convertedUrl = UriToUrl(routeTemplateUri.Uri);
            var decodedUrl = WebUtility.UrlDecode(convertedUrl);

            return decodedUrl;
        }

        private static string BuildKeySegment(IEnumerable<string> keys)
            => string.Join(UrlForwardslash.ToString(), keys);

        private static string UriToUrl(Uri uri)
        {
            var url = $"/{uri.Host}{uri.PathAndQuery}";
            if (ThereIsATrailingForwardSlash())
            {
                url = url.TrimEnd(UrlForwardslash);
            }

            return url;

            bool ThereIsATrailingForwardSlash() => url[url.Length - 1] == UrlForwardslash;
        }
    }
}
