// ***********************************************************************
// Assembly         : Api
// Author           : markco
// Created          : 03-01-2018
//
// Last Modified By : markco
// Last Modified On : 04-11-2018
// ***********************************************************************
// <copyright file="ParameterValidationAttribute.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>ParameterValidationAttribute class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Net;
using Callcredit.AspNetCore.ProblemJson;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace Api
{
    /// <summary>
    /// Provides a type checking constraint for Id parameters.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ActionConstraints.ActionMethodSelectorAttribute" />
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ParameterValidationAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// The identifier parameter name
        /// </summary>
        private readonly string identityParameterName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterValidationAttribute" /> class.
        /// </summary>
        /// <param name="identityParameterName">The parameter name to validate in the request.</param>
        public ParameterValidationAttribute(string identityParameterName)
        {
            this.identityParameterName = identityParameterName;
        }

        /// <summary>
        /// Validates the specified Parameter Id to see if it is a numeric value.
        /// </summary>
        /// <param name="routeContext">The <see cref="RouteContext" />.</param>
        /// <param name="action">The <see cref="ActionDescriptor" />.</param>
        /// <returns><see cref="bool" />.</returns>
        /// <exception cref="ProblemException">when the passed ID is not a numeric value.</exception>
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            var identityParameterValue = routeContext.RouteData.Values[identityParameterName];

            if (int.TryParse((string)identityParameterValue, out var _))
            {
                return true;
            }

            var problem = new Problem(HttpStatusCode.BadRequest)
            {
                Detail = $"Invalid {identityParameterName}.",
                Title = HttpStatusDescription.Get(HttpStatusCode.BadRequest),
                Extensions = new Dictionary<string, object>
                {
                    [identityParameterName] = identityParameterValue
                },
                Instance = new Uri(routeContext.HttpContext.Request.Path.Value, UriKind.Relative)
            };

            throw new ProblemException(problem);
        }
    }
}
