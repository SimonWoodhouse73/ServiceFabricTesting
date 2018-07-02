// ***********************************************************************
// Assembly         : InsolvenciesStepDefinitionsHelper
// Author           : MartinG
// Created          : 03-07-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="AuthorizationTokenGenerator.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>AuthorizationTokenGenerator</summary>
// ***********************************************************************
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using InsolvenciesStepDefinitionsHelper.Data;
using Newtonsoft.Json;

namespace InsolvenciesStepDefinitionsHelper.Helpers
{
    /// <summary>
    /// Class AuthorizationTokenGenerator.
    /// </summary>
    public static class AuthorizationTokenGenerator
    {
        /// <summary>
        /// Gets the authorization token.
        /// </summary>
        /// <param name="authorizationTokenRequest">The authorization token request.</param>
        /// <returns>AuthorizationResponse.</returns>
        public static AuthorizationResponse GetAuthorizationToken(AuthorizationTokenRequest authorizationTokenRequest)
        {
            var audience = authorizationTokenRequest.Audience;
            var authenticationBody =
                $"username={authorizationTokenRequest.AuthenticationRequestBody.Username}&password={authorizationTokenRequest.AuthenticationRequestBody.Password}&organisationalUnitAlias={authorizationTokenRequest.AuthenticationRequestBody.OrganisationalUnitAlias}";

            HttpResponseMessage authenticationResponseMessage = null;
            var authenticationUrl = authorizationTokenRequest.AuthenticationUrl;
            AuthenticationResponse authenticationResponse = null;
            AuthorizationRequest authorizationRequest = null;
            HttpResponseMessage authorizationResponseMessage = null;
            var authorizationUrl = authorizationTokenRequest.AuthorizationUrl;
            var purpose = authorizationTokenRequest.Purpose;
            var result = new AuthorizationResponse();

            using (HttpContent content = new StringContent(authenticationBody, Encoding.UTF8))
            {
                content.Headers.Clear();
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                using (var client = new HttpClient())
                {
                    authenticationResponseMessage = client.PostAsync(authenticationUrl, content).Result;
                }
            }

            if (authenticationResponseMessage.IsSuccessStatusCode)
            {
                authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(
                    authenticationResponseMessage.Content.ReadAsStringAsync().Result);

                authorizationRequest = new AuthorizationRequest()
                {
                    SearchTypeCode = purpose,
                    DataAssetSets = new string[] { audience }
                };

                using (var content = new StringContent(JsonConvert.SerializeObject(authorizationRequest)))
                {
                    content.Headers.Clear();
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", authenticationResponse.AccessToken);
                        authorizationResponseMessage = client.PostAsync(authorizationUrl, content).Result;
                    }
                }

                result = JsonConvert.DeserializeObject<AuthorizationResponse>(authorizationResponseMessage.Content
                    .ReadAsStringAsync().Result);
                result.AccessToken = string.Format("Bearer {0}", result.AccessToken);
            }

            return result;
        }
    }
}
