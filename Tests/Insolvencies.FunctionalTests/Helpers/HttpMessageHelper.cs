// ***********************************************************************
// Assembly         : InsolvenciesStepDefinitionsHelper
// Author           : MartinG
// Created          : 03-07-2018
//
// Last Modified By : MartinG
// Last Modified On : 03-07-2018
// ***********************************************************************
// <copyright file="HttpMessageHelper.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>HttpMessageHelper</summary>
// ***********************************************************************
using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TechTalk.SpecFlow;

namespace InsolvenciesStepDefinitionsHelper.Helpers
{
    /// <summary>
    /// Class HttpMessageHelper.
    /// </summary>
    public class HttpMessageHelper
    {
        /// <summary>
        /// Ignores the bad certificates.
        /// </summary>
        public static void IgnoreBadCertificates()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
        }

        /// <summary>
        /// Creates the request message.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="method">The method.</param>
        /// <returns>HttpRequestMessage.</returns>
        public HttpRequestMessage CreateRequestMessage(string url, string method)
            => new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = new HttpMethod(method)
            };

        /// <summary>
        /// Gets the response message.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="scenarioContext">The scenario context.</param>
        /// <returns>HttpResponseMessage.</returns>
        public HttpResponseMessage GetResponseMessage(HttpRequestMessage request, ScenarioContext scenarioContext)
        {
            IgnoreBadCertificates();

            var domainRoot = scenarioContext[ContextKey.DomainRoot].ToString();
            using (var client = new HttpClient { BaseAddress = new Uri(domainRoot) })
            {
                var responseMessage = client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .GetAwaiter()
                    .GetResult();

                var responseContent = responseMessage.Content.ReadAsStringAsync();
                var resultStream = responseMessage.Content.ReadAsStreamAsync().Result;

                scenarioContext[ContextKey.ResponseContent] = responseContent;
                scenarioContext[ContextKey.Result] = responseContent.Result;
                scenarioContext[ContextKey.ResultContent] = resultStream;

                return responseMessage;
            }
        }

        /// <summary>
        /// Accepts all certifications.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="certification">The certification.</param>
        /// <param name="chain">The chain.</param>
        /// <param name="sslPolicyErrors">The SSL policy errors.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            => true;
    }
}
