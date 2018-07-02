// ***********************************************************************
// Assembly         : InsolvenciesStepDefinitionsHelper
// Author           : MartinG
// Created          : 03-07-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="RequestStepDefinitions.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>RequestStepDefinitions</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using InsolvenciesStepDefinitionsHelper.Data;
using InsolvenciesStepDefinitionsHelper.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace InsolvenciesStepDefinitionsHelper
{
    /// <summary>
    /// Class RequestStepDefinitions.
    /// </summary>
    [Binding]
    public class RequestStepDefinitions
    {
        /// <summary>
        /// The scenario context
        /// </summary>
        private readonly ScenarioContext scenarioContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestStepDefinitions"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenario context.</param>
        public RequestStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        /// <summary>
        /// Givens the default domain root is from configuration.
        /// </summary>
        [Given(@"The default domain root is from App\.config DomainRoot")]
        [Given(@"The default domain root is from configuration")]
        public void GivenTheDefaultDomainRootIsFromConfiguration()
        {
            scenarioContext[ContextKey.DomainRoot] = ConfigurationManager.AppSettings[ContextKey.DomainRoot];
        }

        /// <summary>
        /// Thens the i generate an authorization token.
        /// </summary>
        /// <param name="serviceAudience">The service audience.</param>
        /// <param name="purpose">The purpose.</param>
        [Given(@"I generate an authorization token for (.*) and (.*) permitted purpose")]
        public void ThenIGenerateAnAuthorizationToken(string serviceAudience, string purpose)
        {
            var authenticationRequestBody = new AuthenticationRequestBody
            {
                Username = ConfigurationManager.AppSettings["username"],
                Password = ConfigurationManager.AppSettings["password"],
                OrganisationalUnitAlias = ConfigurationManager.AppSettings["organisationalUnitAlias"]
            };

            var authorizationTokenRequest = new AuthorizationTokenRequest
            {
                Audience = ConfigurationManager.AppSettings[serviceAudience],
                AuthenticationRequestBody = authenticationRequestBody,
                AuthenticationUrl = ConfigurationManager.AppSettings["authenticationURL"],
                AuthorizationUrl = ConfigurationManager.AppSettings["authorizationURL"],
                Purpose = purpose
            };

            var authorisationResponse = AuthorizationTokenGenerator.GetAuthorizationToken(authorizationTokenRequest);

            var headers = this.scenarioContext.GetValueOrDefault<Dictionary<string, string>>(ContextKey.Headers);
            headers.Add("Authorization", authorisationResponse.AccessToken);
            this.scenarioContext[ContextKey.Headers] = headers;
        }

        /// <summary>
        /// Givens the i add headers from configuration.
        /// </summary>
        /// <param name="table">The table.</param>
        [Given(@"I add headers from configuration")]
        public void GivenIAddHeadersFromConfiguration(Table table)
        {
            var headers = scenarioContext.GetValueOrDefault<Dictionary<string, string>>(ContextKey.Headers);

            foreach (var row in table.Rows)
            {
                var key = row[ContextKey.Header];
                var headerParameter = ConfigurationManager.AppSettings[row[ContextKey.Header]];

                if (headers.ContainsKey(key))
                {
                    headers[key] = headerParameter;
                }
                else
                {
                    headers.Add(row[ContextKey.Header], headerParameter);
                }
            }

            scenarioContext[ContextKey.Headers] = headers;
        }

        /// <summary>
        /// Givens the i add includes for types under test.
        /// </summary>
        /// <param name="includes">The includes.</param>
        [Given(@"I add includes for types under test (.*)")]
        [Given(@"I request for (.*)")]
        public void GivenIAddIncludesForTypesUnderTest(string includes)
        {
            scenarioContext[ContextKey.Includes] = includes;
        }

        /// <summary>
        /// Givens the i construct the URL from default domain root and resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        [Given(@"I construct the URL from default DomainRoot and resource (.*)")]
        [Given(@"I construct the URL from default DomainRoot and Resource (.*)")]
        [Given(@"I makeup the URL from default DomainRoot and resource (.*)")]
        [Given(@"I makeup the URL from default DomainRoot and Resource (.*)")]
        public void GivenIConstructTheURLFromDefaultDomainRootAndResource(string resource)
        {
            if (string.IsNullOrEmpty(resource))
            {
                return;
            }

            var domainRoot = scenarioContext[ContextKey.DomainRoot].ToString();

            scenarioContext[ContextKey.Url] = domainRoot + resource;
        }

        /// <summary>
        /// Givens the i construct the URL from default domain root resource and query string.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryString">The query string.</param>
        [Given(@"I construct the URL from default DomainRoot Resource (.*) and QueryString (.*)")]
        public void GivenIConstructTheURLFromDefaultDomainRootResourceAndQueryString(string resource, string queryString)
        {
            if (string.IsNullOrEmpty(resource) || string.IsNullOrEmpty(queryString))
            {
                return;
            }

            var domainRoot = scenarioContext[ContextKey.DomainRoot].ToString();

            scenarioContext[ContextKey.Url] = domainRoot + resource + queryString;
        }

        /// <summary>
        /// Givens the i have a standard request with method get.
        /// </summary>
        [Given(@"I have a standard request with method GET")]
        [Given(@"I have a standard request with the GET method")]
        public void GivenIHaveAStandardRequestWithMethodGet()
        {
            var url = scenarioContext.GetMandatoryValue<string>(ContextKey.Url);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var headers = scenarioContext.GetValueOrDefault<Dictionary<string, string>>(ContextKey.Headers);

            foreach (var item in headers)
            {
                if (requestMessage.Headers.Contains(item.Key))
                {
                    requestMessage.Headers.Remove(item.Key);
                }

                requestMessage.Headers.Add(item.Key, item.Value);
            }

            scenarioContext[ContextKey.Request] = requestMessage;
        }

        /// <summary>
        /// Givens the i have a standard request with method find.
        /// </summary>
        [Given(@"I have a standard request with method FIND")]
        public void GivenIHaveAStandardRequestWithMethodFind()
        {
            var url = scenarioContext.GetMandatoryValue<string>(ContextKey.Url);

            var findHttpMethod = new HttpMethod("FIND");

            var requestMessage = new HttpRequestMessage(findHttpMethod, url);

            var headers = scenarioContext.GetValueOrDefault<Dictionary<string, string>>(ContextKey.Headers);

            foreach (var item in headers)
            {
                if (requestMessage.Headers.Contains(item.Key))
                {
                    requestMessage.Headers.Remove(item.Key);
                }

                requestMessage.Headers.Add(item.Key, item.Value);
            }

            scenarioContext[ContextKey.Request] = requestMessage;
        }

        /// <summary>
        /// Givens the i remove headers.
        /// </summary>
        /// <param name="table">The table.</param>
        [Given(@"I remove headers")]
        [Given(@"I remove the following header parameters from the request")]
        public void GivenIRemoveHeaders(Table table)
        {
            var request = scenarioContext.GetValueOrDefault<HttpRequestMessage>(ContextKey.Request);

            request.Version = HttpVersion.Version10;
            foreach (var row in table.Rows)
            {
                if (row.ContainsKey(ContextKey.Header))
                {
                    request.Headers.Remove(row[ContextKey.Header]);
                }
            }

            scenarioContext[ContextKey.Request] = request;
        }

        /// <summary>
        /// Givens the i add headers with parameters into request.
        /// </summary>
        /// <param name="table">The table.</param>
        [Given(@"I add Headers with Parameters into request")]
        [Given(@"I add the following header parameters to the request")]
        [Given(@"I use a media type not included in the REST standard")]
        public void GivenIAddHeadersWithParametersIntoRequest(Table table)
        {
            var requestMessage = scenarioContext.GetValueOrDefault<HttpRequestMessage>(ContextKey.Request);
            var headers = requestMessage.Headers;
            var content = requestMessage.Content;

            foreach (var row in table.Rows)
            {
                var key = row[ContextKey.Header];
                var value = row[ContextKey.Parameter];

                if (key == "Content-Type")
                {
                    if (content == null)
                    {
                        content = new StringContent(string.Empty);
                    }

                    if (content.Headers.Contains(key))
                    {
                        content.Headers.Remove(key);
                    }

                    content.Headers.Add(key, value);
                }
                else
                {
                    if (headers.Contains(key))
                    {
                        headers.Remove(key);
                    }

                    headers.Add(key, value);
                }
            }

            scenarioContext[ContextKey.Request] = requestMessage;
        }

        /// <summary>
        /// Whens the i send a request.
        /// </summary>
        [When(@"I send a request")]
        public void WhenISendARequest()
        {
            var httpMessageHelper = new HttpMessageHelper();

            var request = this.scenarioContext.GetValueOrDefault<HttpRequestMessage>(ContextKey.Request);
            var responseMessage = httpMessageHelper.GetResponseMessage(request, scenarioContext);

            scenarioContext[ContextKey.ResponseMessage] = responseMessage;
            scenarioContext[ContextKey.StatusCode] = responseMessage.StatusCode;

            if (!responseMessage.IsSuccessStatusCode)
            {
                var result = (string)scenarioContext[ContextKey.Result];

                Assert.IsFalse(string.IsNullOrWhiteSpace(result), $"Expected response to have a result. {request.RequestUri.PathAndQuery}");

                var errorMessageContent = JsonConvert.DeserializeObject<ErrorMessage>(result);
                var jsonErrorMessage = JsonConvert.DeserializeObject<ErrorResponseBody>(result);

                if ((int)responseMessage.StatusCode <= 500)
                {
                    scenarioContext[ContextKey.ErrorMessage] = jsonErrorMessage.Detail;
                }
                else
                {
                    scenarioContext[ContextKey.ErrorMessage] = errorMessageContent.Message;
                }
            }
        }

        /// <summary>
        /// Whens the i call the insolvencies service with no date.
        /// </summary>
        [When(@"I call the Insolvencies Service")]
        public void WhenICallTheInsolvenciesServiceWithNoDate()
        {
            WhenICallTheInsolvenciesService(OperationDateTypes.Current);
        }

        /// <summary>
        /// Whens the i call the insolvencies service.
        /// </summary>
        /// <param name="searchDate">The search date.</param>
        /// <exception cref="ArgumentException">Thrown if the searchDate is not in a recognised format.</exception>
        [When(@"I call the Insolvencies Service based on (.*) date")]
        public void WhenICallTheInsolvenciesService(string searchDate)
        {
            var url = scenarioContext.GetMandatoryValue<string>(ContextKey.Url);
            var httpMessageHelper = new HttpMessageHelper();
            var requestMessage = httpMessageHelper.CreateRequestMessage(url, "GET");
            var headers = scenarioContext.GetValueOrDefault<Dictionary<string, string>>(ContextKey.Headers);

            foreach (var item in headers)
            {
                requestMessage.Headers.Add(item.Key, item.Value);
            }

            if (searchDate == null)
            {
                searchDate = "current";
            }

            switch (searchDate)
            {
                case OperationDateTypes.Current:
                    break;
                case OperationDateTypes.CurrentMinus0Days:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.ToString("yyyy-MM-dd"));
                    break;
                case OperationDateTypes.CurrentMinus1Day:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
                    break;
                case OperationDateTypes.CurrentMinus1Year:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"));
                    break;
                case OperationDateTypes.CurrentMinus2Years:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd"));
                    break;
                case OperationDateTypes.CurrentMinus3Years:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.AddYears(-3).ToString("yyyy-MM-dd"));
                    break;
                case OperationDateTypes.CurrentMinus4Years:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.AddYears(-4).ToString("yyyy-MM-dd"));
                    break;
                case OperationDateTypes.CurrentMinus5Years:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.AddYears(-5).ToString("yyyy-MM-dd"));
                    break;
                case OperationDateTypes.CurrentMinus6Years:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.AddYears(-6).ToString("yyyy-MM-dd"));
                    break;
                case OperationDateTypes.CurrentMinus7Years:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.AddYears(-7).ToString("yyyy-MM-dd"));
                    break;
                case OperationDateTypes.CurrentMinus8Years:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.AddYears(-8).ToString("yyyy-MM-dd"));
                    break;
                case OperationDateTypes.CurrentMinus9Years:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.AddYears(-9).ToString("yyyy-MM-dd"));
                    break;
                case OperationDateTypes.CurrentPlus6Years:
                    requestMessage.Headers.Add("Operation-Date", DateTime.Now.AddYears(6).ToString("yyyy-MM-dd"));
                    break;
                default:
                    throw new ArgumentException($"search Date Type {searchDate} is not in a list of Operation Date Types");
            }

            scenarioContext[ContextKey.Request] = requestMessage;

            var request = scenarioContext.GetValueOrDefault<HttpRequestMessage>(ContextKey.Request);
            var responseMessage = httpMessageHelper.GetResponseMessage(request, scenarioContext);

            scenarioContext[ContextKey.ResponseMessage] = responseMessage;
            scenarioContext[ContextKey.StatusCode] = responseMessage.StatusCode;

            if (responseMessage.IsSuccessStatusCode)
            {
                return;
            }

            var result = (string)this.scenarioContext[ContextKey.Result];

            Assert.IsFalse(string.IsNullOrWhiteSpace(result), $"Expected response to have a result. {request.RequestUri.PathAndQuery}");
        }
    }
}
