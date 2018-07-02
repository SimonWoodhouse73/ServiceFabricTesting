// ***********************************************************************
// Assembly         : InsolvenciesStepDefinitionsHelper
// Author           : MartinG
// Created          : 03-07-2018
//
// Last Modified By : MartinG
// Last Modified On : 03-07-2018
// ***********************************************************************
// <copyright file="ScenarioContextExtensions.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>ScenarioContextExtensions</summary>
// ***********************************************************************
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace InsolvenciesStepDefinitionsHelper.Helpers
{
    /// <summary>
    /// Class ScenarioContextExtensions.
    /// </summary>
    public static class ScenarioContextExtensions
    {
        /// <summary>
        /// Gets the value or default.
        /// </summary>
        /// <typeparam name="TInstance">The type of the t instance.</typeparam>
        /// <param name="scenarioContext">The scenario context.</param>
        /// <param name="key">The key.</param>
        /// <returns>TInstance.</returns>
        public static TInstance GetValueOrDefault<TInstance>(this ScenarioContext scenarioContext, string key)
            where TInstance : new()
        {
            if (scenarioContext.ContainsKey(key))
            {
                return (TInstance)scenarioContext[key];
            }

            return new TInstance();
        }

        /// <summary>
        /// Gets the mandatory value.
        /// </summary>
        /// <typeparam name="TInstance">The type of the t instance.</typeparam>
        /// <param name="scenarioContext">The scenario context.</param>
        /// <param name="key">The key.</param>
        /// <returns>TInstance.</returns>
        public static TInstance GetMandatoryValue<TInstance>(this ScenarioContext scenarioContext, string key)
        {
            if (!scenarioContext.ContainsKey(key))
            {
                Assert.Fail($"Scenario context does not contain mandatory value for key ({key}).");
            }

            return (TInstance)scenarioContext[key];
        }
    }
}
