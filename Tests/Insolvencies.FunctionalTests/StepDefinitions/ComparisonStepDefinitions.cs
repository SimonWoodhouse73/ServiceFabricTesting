// ***********************************************************************
// Assembly         : InsolvenciesStepDefinitionsHelper
// Author           : MartinG
// Created          : 03-14-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-21-2018
// ***********************************************************************
// <copyright file="ComparisonStepDefinitions.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>Common step definitions for use with Specflow tests. </summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static InsolvenciesStepDefinitionsHelper.InsolvenciesResponseBody;

namespace InsolvenciesStepDefinitionsHelper
{
    /// <summary>
    /// Class ComparisonStepDefinitions.
    /// </summary>
    [Binding]
    public class ComparisonStepDefinitions
    {
        /// <summary>
        /// The date time format
        /// </summary>
        private const string DateTimeFormat = "yyyy-MM-dd";

        /// <summary>
        /// The specflow null value
        /// </summary>
        private const string SpecflowNullValue = "NULL";

        /// <summary>
        /// The result resources
        /// </summary>
        private readonly Dictionary<string, Func<IEnumerable>> resultResources;

        /// <summary>
        /// The scenario context
        /// </summary>
        private readonly ScenarioContext scenarioContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonStepDefinitions"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenario context.</param>
        public ComparisonStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;

            resultResources = new Dictionary<string, Func<IEnumerable>>
            {
                [nameof(InsolvencyOrder)] = () => GetInsolvenciesFromResult(),
                [nameof(InsolvencyOrderType)] = () => GetInsolvencyTypesFromResult(),
                [nameof(InsolvencyOrderRestrictionsType)] = () => GetInsolvencyRestrictionsTypeFromResult(),
                [nameof(InsolvencyOrderPerson)] = () => GetInsolvencyPersonFromResult(),
                [nameof(InsolvencyOrderAddress)] = () => GetInsolvencyAddressesFromResult(),
                [nameof(Dispute)] = () => GetInsolvencyDisputeFromResult(),
                [nameof(InsolvencyOrderHistory)] = () => GetInsolvencyHistoryFromResult(),
                [nameof(InsolvencyOrderTradingDetails)] = () => GetInsolvencyTradingDetailFromResult(),
                [nameof(InsolvencyOrderEmbeddedResources)] = () => GetEmbeddedInsolvencyResources(),
                [nameof(InsolvencyOrderLinks)] = () => GetInsolvencyLinks(),
                [nameof(InsolvencyOrderPersonLinks)] = () => GetInsolvencyPersonLinks(),
                [nameof(DisputeLinks)] = () => GetInsolvencyDisputeLinks(),
                [nameof(InsolvencyOrderTradingDetailsLinks)] = () => GetInsolvencyTradingDetailLinks(),
                [nameof(InsolvencyOrderAddressLinks)] = () => GetInsolvencyAddressLinks(),
                [nameof(InsolvencyOrderHistoryLinks)] = () => GetInsolvencyHistoryLinks()
            };
        }

        /// <summary>
        /// Givens the i have a residence identifier.
        /// </summary>
        /// <param name="residenceId">The residence identifier.</param>
        [Given(@"I have a ResidenceId (\d+)")]
        public void GivenIHaveAResidenceId(int residenceId)
        {
            SetUrl(residenceId);
        }

        /// <summary>
        /// Thens the response returned should contain an inclusion error message.
        /// </summary>
        [Then(@"the response returned should contain an Inclusion error message")]
        public void ThenTheResponseReturnedShouldContainAnInclusionErrorMessage()
        {
            var includes = scenarioContext[ContextKey.Includes];
            var result = (string)scenarioContext[ContextKey.Result];
            var jsonErrorMessage = JsonConvert.DeserializeObject<ErrorResponseBody>(result);
            Assert.AreEqual($"The specified inclusion '{includes}' is not in the list of valid inclusions.", jsonErrorMessage.Detail);
        }

        /// <summary>
        /// Thens the response returned should contain records.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        [Then(@"the response returned should contain (.*) records")]
        public void ThenTheResponseReturnedShouldContainRecords(string resourceName)
        {
            var hasAnyRecords = HasAnyItems(resourceName);
            Assert.IsTrue(hasAnyRecords);
        }

        /// <summary>
        /// Thens the response returned should contain the embedded resources for the embedded resource.
        /// </summary>
        /// <param name="commaDelimetedEmbeddedResources">The comma delimeted embedded resources.</param>
        [Then(@"the response returned should contain the EmbeddedResources (.*)")]
        public void ThenTheResponseReturnedShouldContainTheEmbeddedResourcesForTheEmbeddedResource(string commaDelimetedEmbeddedResources)
        {
            AssertEmbeddedResources(commaDelimetedEmbeddedResources, shouldExist: true);
        }

        /// <summary>
        /// Thens the response returned should contain the following insolvency links data.
        /// </summary>
        /// <param name="expectedData">The expected data.</param>
        [Then(@"the response returned should contain the following InsolvencyLinks data")]
        public void ThenTheResponseReturnedShouldContainTheFollowingInsolvencyLinksData(Table expectedData)
        {
            AssertResultContainsExpectedData<InsolvencyOrderLinks>(expectedData);
        }

        /// <summary>
        /// Thens the response returned should contain the following insolvency type data.
        /// </summary>
        /// <param name="expectedData">The expected data.</param>
        [Then(@"the response returned should contain the following InsolvencyType data")]
        public void ThenTheResponseReturnedShouldContainTheFollowingInsolvencyTypeData(Table expectedData)
        {
            AssertResultContainsExpectedData<InsolvencyOrderType>(expectedData);
        }

        /// <summary>
        /// Thens the response returned should contain the following restrictions type data.
        /// </summary>
        /// <param name="expectedData">The expected data.</param>
        [Then(@"the response returned should contain the following RestrictionsType data")]
        public void ThenTheResponseReturnedShouldContainTheFollowingRestrictionsTypeData(Table expectedData)
        {
            AssertResultContainsExpectedData<InsolvencyOrderRestrictionsType>(expectedData);
        }

        /// <summary>
        /// Thens the response returned should have the correct amount of records.
        /// </summary>
        /// <param name="expectedRecords">The expected records.</param>
        /// <param name="resourceName">Name of the resource.</param>
        [Then(@"the response returned should have (\d+) (.*) records")]
        public void ThenTheResponseReturnedShouldHaveTheCorrectAmountOfRecords(int expectedRecords, string resourceName)
        {
            var actualRecords = CountItems(resourceName);
            Assert.AreEqual(expectedRecords, actualRecords);
        }

        /// <summary>
        /// Thens the response returned should match the following address links data.
        /// </summary>
        /// <param name="expectedData">The expected data.</param>
        [Then(@"the response returned should match the following AddressLinks data")]
        public void ThenTheResponseReturnedShouldMatchTheFollowingAddressLinksData(Table expectedData)
        {
            AssertResultMatchesExpectedData<InsolvencyOrderAddressLinks>(expectedData);
        }

        /// <summary>
        /// Thens the response returned should match the following dispute links data.
        /// </summary>
        /// <param name="expectedData">The expected data.</param>
        [Then(@"the response returned should match the following DisputeLinks data")]
        public void ThenTheResponseReturnedShouldMatchTheFollowingDisputeLinksData(Table expectedData)
        {
            AssertResultMatchesExpectedData<DisputeLinks>(expectedData);
        }

        /// <summary>
        /// Thens the response returned should match the following history links data.
        /// </summary>
        /// <param name="expectedData">The expected data.</param>
        [Then(@"the response returned should match the following HistoryLinks data")]
        public void ThenTheResponseReturnedShouldMatchTheFollowingHistoryLinksData(Table expectedData)
        {
            AssertResultMatchesExpectedData<InsolvencyOrderHistoryLinks>(expectedData);
        }

        /// <summary>
        /// Thens the response returned should match the following insolvency links data.
        /// </summary>
        /// <param name="expectedData">The expected data.</param>
        [Then(@"the response returned should match the following InsolvencyLinks data")]
        public void ThenTheResponseReturnedShouldMatchTheFollowingInsolvencyLinksData(Table expectedData)
        {
            AssertResultMatchesExpectedData<InsolvencyOrderLinks>(expectedData);
        }

        /// <summary>
        /// Thens the response returned should match the following insolvency order data.
        /// </summary>
        /// <param name="expectedData">The expected data.</param>
        [Then(@"the response returned should match the following InsolvencyOrder data")]
        public void ThenTheResponseReturnedShouldMatchTheFollowingInsolvencyOrderData(Table expectedData)
        {
            AssertResultMatchesExpectedData<InsolvencyOrder>(expectedData);
        }

        /// <summary>
        /// Thens the response returned should match the following insolvency type data.
        /// </summary>
        /// <param name="expectedData">The expected data.</param>
        [Then(@"the response returned should match the following InsolvencyType data")]
        public void ThenTheResponseReturnedShouldMatchTheFollowingInsolvencyTypeData(Table expectedData)
        {
            AssertResultMatchesExpectedData<InsolvencyOrderType>(expectedData);
        }

        /// <summary>
        /// Thens the response returned should match the following person links data.
        /// </summary>
        /// <param name="expectedData">The expected data.</param>
        [Then(@"the response returned should match the following PersonLinks data")]
        public void ThenTheResponseReturnedShouldMatchTheFollowingPersonLinksData(Table expectedData)
        {
            AssertResultMatchesExpectedData<InsolvencyOrderPersonLinks>(expectedData);
        }

        /// <summary>
        /// Thens the response returned should match the following trading detail links data.
        /// </summary>
        /// <param name="expectedData">The expected data.</param>
        [Then(@"the response returned should match the following TradingDetailLinks data")]
        public void ThenTheResponseReturnedShouldMatchTheFollowingTradingDetailLinksData(Table expectedData)
        {
            AssertResultMatchesExpectedData<InsolvencyOrderTradingDetailsLinks>(expectedData);
        }

        /// <summary>
        /// Thens the response returned should not contain the embedded resources for the embedded resource.
        /// </summary>
        /// <param name="commaDelimetedEmbeddedResources">The comma delimeted embedded resources.</param>
        [Then(@"the response returned should not contain the EmbeddedResources (.*)")]
        public void ThenTheResponseReturnedShouldNotContainTheEmbeddedResourcesForTheEmbeddedResource(string commaDelimetedEmbeddedResources)
        {
            AssertEmbeddedResources(commaDelimetedEmbeddedResources, shouldExist: false);
        }

        /// <summary>
        /// Thens the response returned should not have any records.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        [Then(@"the response returned should not have any (.*) records")]
        public void ThenTheResponseReturnedShouldNotHaveAnyRecords(string resourceName)
        {
            var hasAnyRecords = HasAnyItems(resourceName);
            Assert.IsFalse(hasAnyRecords);
        }

        /// <summary>
        /// Whens the i use embedded resource collection.
        /// </summary>
        /// <param name="embeddedResourceCollectionName">Name of the embedded resource collection.</param>
        [When(@"I use EmbeddedResourceCollection (.*)")]
        public void WhenIUseEmbeddedResourceCollection(string embeddedResourceCollectionName)
        {
            var embeddedResourceCollection = resultResources[embeddedResourceCollectionName].Invoke();
            scenarioContext[ContextKey.EmbeddedResourceCollection] = embeddedResourceCollection;
        }

        /// <summary>
        /// Doeses the result contain data.
        /// </summary>
        /// <typeparam name="TResource">The type of the t resource.</typeparam>
        /// <param name="expectedValues">The expected values.</param>
        /// <param name="actualObjects">The actual objects.</param>
        /// <param name="fieldsWithoutMatch">The fields without match.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool DoesResultContainData<TResource>(
            IDictionary<string, string> expectedValues,
            IEnumerable<TResource> actualObjects,
            Dictionary<string, ICollection<string>> fieldsWithoutMatch)
        {
            foreach (var actualObject in actualObjects)
            {
                var isMatch = false;

                foreach (var expectedValuePair in expectedValues)
                {
                    var fieldName = expectedValuePair.Key;
                    var expectedValue = expectedValuePair.Value;
                    var actualValue = GetValue(actualObject, fieldName);

                    isMatch = actualValue == null ?
                        IsSpecflowNullValue(expectedValue) :
                        actualValue.Equals(expectedValue);

                    if (!isMatch)
                    {
                        UpdateFieldsWithoutMatchDictionary(fieldsWithoutMatch, fieldName, expectedValue, actualValue);
                        break;
                    }
                }

                if (isMatch)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentNullException">Thrown if propertyName is not found in the model.</exception>
        private static string GetValue(object model, string propertyName)
        {
            try
            {
                return model
                    .GetType()
                    .GetProperty(propertyName)
                    .GetValue(model)
                    ?.ToString();
            }
            catch (NullReferenceException exception)
            {
                var message = $"Failed to get the value {propertyName} from the object {model.GetType().Name}.\nException Message: {exception.Message}";
                throw new ArgumentNullException(message);
            }
        }

        /// <summary>
        /// Determines whether [is specflow null value] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is specflow null value] [the specified value]; otherwise, <c>false</c>.</returns>
        private static bool IsSpecflowNullValue(string value)
        => value == SpecflowNullValue || value == string.Empty;

        /// <summary>
        /// Updates the fields without match dictionary.
        /// </summary>
        /// <param name="fieldsWithoutMatch">The fields without match.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="expectedValue">The expected value.</param>
        /// <param name="actualValue">The actual value.</param>
        private static void UpdateFieldsWithoutMatchDictionary(
                IDictionary<string, ICollection<string>> fieldsWithoutMatch,
                string fieldName,
                string expectedValue,
                string actualValue)
        {
            ICollection<string> expectedValuesCollection;

            if (fieldsWithoutMatch.ContainsKey(fieldName))
            {
                expectedValuesCollection = fieldsWithoutMatch[fieldName];
            }
            else
            {
                expectedValuesCollection = new List<string>();
                fieldsWithoutMatch.Add(fieldName, expectedValuesCollection);
            }

            actualValue = actualValue ?? SpecflowNullValue;
            var message = $"Expected: '{expectedValue}' | Actual: '{actualValue}'";
            if (!expectedValuesCollection.Contains(message))
            {
                expectedValuesCollection.Add(message);
            }
        }

        /// <summary>
        /// Asserts the embedded resources.
        /// </summary>
        /// <param name="commaDelimetedEmbeddedResources">The comma delimeted embedded resources.</param>
        /// <param name="shouldExist">if set to <c>true</c> [should exist].</param>
        /// <exception cref="AssertFailedException">Thrown if any of the embedded resources are set to null.</exception>
        private void AssertEmbeddedResources(string commaDelimetedEmbeddedResources, bool shouldExist)
        {
            if (string.IsNullOrWhiteSpace(commaDelimetedEmbeddedResources))
            {
                return;
            }

            var actualEmbeddedResources = (IEnumerable)scenarioContext[ContextKey.EmbeddedResourceCollection];

            var expectedEmbeddedResources = commaDelimetedEmbeddedResources.Split(',');

            foreach (var actualEmbeddedResource in actualEmbeddedResources)
            {
                if (actualEmbeddedResource != null)
                {
                    foreach (var expectedEmbeddedResource in expectedEmbeddedResources)
                    {
                        var resource = GetValue(actualEmbeddedResource, expectedEmbeddedResource);
                        var resourceExists = resource != null;

                        if (shouldExist == resourceExists)
                        {
                            continue;
                        }

                        var existErrorMessage = shouldExist
                            ? "was expected but was null for at least one of the resources."
                            : "was not expected but was not null for at least on of the resources.";

                        Assert.Fail($"The resource '{expectedEmbeddedResource}' {existErrorMessage}");
                    }
                }
                else
                {
                    if (!shouldExist)
                    {
                        continue;
                    }

                    var message = $"At least one embedded resource collection was null but should not be.";
                    throw new AssertFailedException(message);
                }
            }
        }

        /// <summary>
        /// Asserts the result contains expected data.
        /// </summary>
        /// <typeparam name="TResource">The type of the t resource.</typeparam>
        /// <param name="expectedData">The expected data.</param>
        private void AssertResultContainsExpectedData<TResource>(Table expectedData)
        {
            expectedData = UpdateDataInTable(expectedData);

            var rowsWithoutMatch = new List<int>();
            var resourcesInResult = GetResourcesFromResult<TResource>();
            var fieldsWithoutMatch = new Dictionary<string, ICollection<string>>();

            for (int rowIndex = 0; rowIndex < expectedData.RowCount; rowIndex++)
            {
                var expectedRow = expectedData.Rows[rowIndex];
                var rowExists = DoesResultContainData(expectedRow, resourcesInResult, fieldsWithoutMatch);

                if (!rowExists)
                {
                    rowsWithoutMatch.Add(rowIndex + 1);
                }
            }

            if (rowsWithoutMatch.Any())
            {
                var formattedOutput = FormatOutput(rowsWithoutMatch, fieldsWithoutMatch);

                Assert.Fail(formattedOutput);
            }
        }

        /// <summary>
        /// Asserts the result matches expected data.
        /// </summary>
        /// <typeparam name="TResource">The type of the t resource.</typeparam>
        /// <param name="expectedData">The expected data.</param>
        private void AssertResultMatchesExpectedData<TResource>(Table expectedData)
        {
            var actualData = GetResourcesFromResult<TResource>();
            expectedData = UpdateDataInTable(expectedData);
            expectedData.CompareToSet(actualData);
        }

        /// <summary>
        /// Calculates the date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private string CalculateDate(string value)
        {
            if (string.IsNullOrEmpty(value) || value == SpecflowNullValue)
            {
                return value;
            }

            var changeableDateFormat = @"(-?\d+) months (-?\d+) days old";
            var regex = Regex.Match(value, changeableDateFormat);

            if (!regex.Success)
            {
                return value;
            }

            var months = int.Parse(regex.Groups[1].Value);
            var days = int.Parse(regex.Groups[2].Value);

            return DateTime.Now.Date
                .AddMonths(-months)
                .AddDays(-days)
                .ToString(DateTimeFormat);
        }

        /// <summary>
        /// Counts the items.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>System.Int32.</returns>
        private int CountItems(string resourceName)
        {
            var resources = GetResourcesFromResult(resourceName);
            var enumerator = resources.GetEnumerator();
            var count = 0;

            while (enumerator.MoveNext())
            {
                count++;
            }

            return count;
        }

        /// <summary>
        /// Formats the output.
        /// </summary>
        /// <param name="rowsWithoutMatch">The rows without match.</param>
        /// <param name="fieldsWithoutMatch">The fields without match.</param>
        /// <returns>System.String.</returns>
        private string FormatOutput(List<int> rowsWithoutMatch, Dictionary<string, ICollection<string>> fieldsWithoutMatch)
        {
            var message = $"Match not found for the following rows: {string.Join(",", rowsWithoutMatch.ToArray())} \n\nFields:\n";
            var output = new StringBuilder(message);

            foreach (var fieldWithoutMatch in fieldsWithoutMatch)
            {
                output.AppendLine($"{fieldWithoutMatch.Key}:");

                foreach (var valueWithoutMatch in fieldWithoutMatch.Value)
                {
                    output.AppendLine(valueWithoutMatch);
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// Gets the embedded insolvency resources.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderEmbeddedResources&gt;.</returns>
        private IEnumerable<InsolvencyOrderEmbeddedResources> GetEmbeddedInsolvencyResources()
        {
            var insolvencyOrdersFromResult = GetInsolvenciesFromResult();
            return
                insolvencyOrdersFromResult
                .Select(account => account.InsolvencyOrderEmbeddedResources);
        }

        /// <summary>
        /// Gets the insolvencies from result.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrder&gt;.</returns>
        private IEnumerable<InsolvencyOrder> GetInsolvenciesFromResult()
        {
            return GetRootObjectFromResult()
                .EmbeddedRootResources.InsolvencyOrders;
        }

        /// <summary>
        /// Gets the insolvency addresses from result.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderAddress&gt;.</returns>
        private IEnumerable<InsolvencyOrderAddress> GetInsolvencyAddressesFromResult()
        {
            var insolvencyLastKnownAddress =
                GetInsolvenciesFromResult()
                .SelectMany(account => account.InsolvencyOrderEmbeddedResources.InsolvencyOrderAddresses);

            return insolvencyLastKnownAddress;
        }

        /// <summary>
        /// Gets the insolvency address from result.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderAddress&gt;.</returns>
        private IEnumerable<InsolvencyOrderAddress> GetInsolvencyAddressFromResult()
        {
            ////CheckRequestContainsIncludes<InsolvencyAddress>();

            var insolvencyAddress = GetInsolvenciesFromResult()
                .SelectMany(account => account.InsolvencyOrderEmbeddedResources.InsolvencyOrderAddresses);

            return insolvencyAddress;
        }

        /// <summary>
        /// Gets the insolvency address links.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderAddressLinks&gt;.</returns>
        private IEnumerable<InsolvencyOrderAddressLinks> GetInsolvencyAddressLinks()
        {
            var insolvencyAddressLinks = GetInsolvencyAddressFromResult()
                .Select(address => address.InsolvencyOrderAddressLinks);

            return insolvencyAddressLinks;
        }

        /// <summary>
        /// Gets the insolvency dispute from result.
        /// </summary>
        /// <returns>IEnumerable&lt;Dispute&gt;.</returns>
        private IEnumerable<Dispute> GetInsolvencyDisputeFromResult()
        {
            ////CheckRequestContainsIncludes<InsolvencyDispute>();

            var insolvencyDispute = GetInsolvenciesFromResult()
                .SelectMany(account => account.InsolvencyOrderEmbeddedResources.Disputes);

            return insolvencyDispute;
        }

        /// <summary>
        /// Gets the insolvency dispute links.
        /// </summary>
        /// <returns>IEnumerable&lt;DisputeLinks&gt;.</returns>
        private IEnumerable<DisputeLinks> GetInsolvencyDisputeLinks()
        {
            var insolvencyDisputeLinks = GetInsolvencyDisputeFromResult()
                .Select(dispute => dispute.DisputeLinks);

            return insolvencyDisputeLinks;
        }

        /// <summary>
        /// Gets the insolvency history from result.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderHistory&gt;.</returns>
        private IEnumerable<InsolvencyOrderHistory> GetInsolvencyHistoryFromResult()
        {
            ////CheckRequestContainsIncludes<InsolvencyHistory>();

            var insolvencyHistory = GetInsolvenciesFromResult()
                .SelectMany(account => account.InsolvencyOrderEmbeddedResources.InsolvencyOrderHistories);

            return insolvencyHistory;
        }

        /// <summary>
        /// Gets the insolvency history links.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderHistoryLinks&gt;.</returns>
        private IEnumerable<InsolvencyOrderHistoryLinks> GetInsolvencyHistoryLinks()
        {
            var insolvencyHistoryLinks = GetInsolvencyHistoryFromResult()
                .Select(history => history.InsolvencyOrderHistoryLinks);

            return insolvencyHistoryLinks;
        }

        /// <summary>
        /// Gets the insolvency links.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderLinks&gt;.</returns>
        private IEnumerable<InsolvencyOrderLinks> GetInsolvencyLinks()
        {
            var insolvencyLinks = GetInsolvenciesFromResult()
                .Select(account => account.InsolvencyOrderLinks);

            return insolvencyLinks;
        }

        /// <summary>
        /// Gets the insolvency person from result.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderPerson&gt;.</returns>
        private IEnumerable<InsolvencyOrderPerson> GetInsolvencyPersonFromResult()
        {
            ////CheckRequestContainsIncludes<InsolvencyPerson>();

            var insolvencyPerson = GetInsolvenciesFromResult()
                .SelectMany(account => account.InsolvencyOrderEmbeddedResources.InsolvencyOrderPersons);

            return insolvencyPerson;
        }

        ////    return insolvencyOrderStatus;
        ////}
        /// <summary>
        /// Gets the insolvency person links.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderPersonLinks&gt;.</returns>
        private IEnumerable<InsolvencyOrderPersonLinks> GetInsolvencyPersonLinks()
        {
            var insolvencyPersonLinks = GetInsolvencyPersonFromResult()
                .Select(person => person.PersonLinks);

            return insolvencyPersonLinks;
        }

        /// <summary>
        /// Gets the insolvency restrictions type from result.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderRestrictionsType&gt;.</returns>
        private IEnumerable<InsolvencyOrderRestrictionsType> GetInsolvencyRestrictionsTypeFromResult()
        {
            var insolvencyRestrictionsType = GetInsolvenciesFromResult()
                .Select(account => account.RestrictionsType);

            return insolvencyRestrictionsType;
        }

        /// <summary>
        /// Gets the insolvency trading detail from result.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderTradingDetails&gt;.</returns>
        private IEnumerable<InsolvencyOrderTradingDetails> GetInsolvencyTradingDetailFromResult()
        {
            ////CheckRequestContainsIncludes<InsolvencyTradingDetail>();

            var insolvencyOrdersFromResult = GetInsolvenciesFromResult();
            var insolvencyTradingDetail =
                insolvencyOrdersFromResult
                .SelectMany(account => account.InsolvencyOrderEmbeddedResources.InsolvencyOrderTradingDetailsEmbedded);

            return insolvencyTradingDetail;
        }

        ////private IEnumerable<InsolvencyOrderStatus> GetInsolvencyOrderStatusFromResult()
        ////{
        ////    var insolvencyOrderStatus = GetInsolvenciesFromResult()
        ////        .SelectMany(account => account.EmbeddedInsolvencyResources.);
        /// <summary>
        /// Gets the insolvency trading detail links.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderTradingDetailsLinks&gt;.</returns>
        private IEnumerable<InsolvencyOrderTradingDetailsLinks> GetInsolvencyTradingDetailLinks()
        {
            var insolvencyTradingDetailLinks = GetInsolvencyTradingDetailFromResult()
                .Select(tradingDetail => tradingDetail.InsolvencyOrderTradingDetailsLinks);

            return insolvencyTradingDetailLinks;
        }

        ////private EmbeddedRootResources GetEmbeddedRootResources()
        ////{
        ////    return GetRootObjectFromResult()
        ////        .EmbeddedRootResources;
        ////}
        /// <summary>
        /// Gets the insolvency types from result.
        /// </summary>
        /// <returns>IEnumerable&lt;InsolvencyOrderType&gt;.</returns>
        private IEnumerable<InsolvencyOrderType> GetInsolvencyTypesFromResult()
        {
            var insolvencyTypes = GetInsolvenciesFromResult()
                .Select(account => account.InsolvencyOrderType);

            return insolvencyTypes;
        }

        /// <summary>
        /// Gets the resources from result.
        /// </summary>
        /// <typeparam name="TResource">The type of the t resource.</typeparam>
        /// <returns>IEnumerable&lt;TResource&gt;.</returns>
        private IEnumerable<TResource> GetResourcesFromResult<TResource>()
        {
            var resourceName = typeof(TResource).Name;
            var resources = GetResourcesFromResult(resourceName);

            return (IEnumerable<TResource>)resources;
        }

        /// <summary>
        /// Gets the resources from result.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>IEnumerable.</returns>
        /// <exception cref="ArgumentException">Thrown if resourceName is not found in the results.</exception>
        private IEnumerable GetResourcesFromResult(string resourceName)
        {
            if (!resultResources.ContainsKey(resourceName))
            {
                var message =
                   $"The resource name '{resourceName}' doesn't exist in the result resources dictionary. " +
                    "It's either been mispelt or needs to be added.";

                throw new ArgumentException(message);
            }

            return resultResources[resourceName].Invoke();
        }

        /// <summary>
        /// Gets the root object from result.
        /// </summary>
        /// <returns>RootObject.</returns>
        /// <exception cref="FormatException">Thrown if the JSon result does not contain any embedded resources.</exception>
        private RootObject GetRootObjectFromResult()
        {
            var result = (string)scenarioContext[ContextKey.Result];

            var rootObject = JsonConvert.DeserializeObject<RootObject>(result);

            if (rootObject.EmbeddedRootResources == null)
            {
                var message =
                    $"Could not deserialize the result:\n" +
                    $"Result: {result}";

                throw new FormatException(message);
            }

            return rootObject;
        }

        /// <summary>
        /// Determines whether [has any items] [the specified resource name].
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns><c>true</c> if [has any items] [the specified resource name]; otherwise, <c>false</c>.</returns>
        private bool HasAnyItems(string resourceName)
        {
            var resources = GetResourcesFromResult(resourceName);
            var enumerator = resources.GetEnumerator();
            return enumerator.MoveNext();
        }

        /// <summary>
        /// Removes the single quotation marks.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private string RemoveSingleQuotationMarks(string value)
        {
            const char quotationMark = '\'';

            var firstQuotationIndex = 0;
            var lastQuotationIndex = value.Length - 1;

            if (value.IndexOf(quotationMark) == firstQuotationIndex &&
                value.LastIndexOf(quotationMark) == lastQuotationIndex)
            {
                return value.Remove(lastQuotationIndex).Remove(firstQuotationIndex, 1);
            }

            return value;
        }

        /// <summary>
        /// Sets the URL.
        /// </summary>
        /// <param name="residenceId">The residence identifier.</param>
        private void SetUrl(int residenceId)
        {
            var includes = scenarioContext.ContainsKey(ContextKey.Includes) ?
                "?include=" + scenarioContext[ContextKey.Includes] :
                string.Empty;

            scenarioContext[ContextKey.Url] = scenarioContext[ContextKey.DomainRoot]
                + $"api/uk/population-map/residences/{residenceId}/insolvency-orders"
                + includes;
        }

        ////private void CheckRequestContainsIncludes<TType>()
        ////{
        ////    var expectedInclude = typeof(TType).Name;

        ////    if (scenarioContext.ContainsKey(ContextKey.Includes))
        ////    {
        ////        var actualInclude = (string)scenarioContext[ContextKey.Includes];

        ////        if (actualInclude.IndexOf(expectedInclude, StringComparison.OrdinalIgnoreCase) >= 0)
        ////        {
        ////            return;
        ////        }
        ////    }

        ////    var message = $"Includes for type {typeof(TType).Name} wasn't included.";
        ////    throw new ArgumentException(message);
        ////}

        /// <summary>
        /// Updates the data in table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>Table.</returns>
        private Table UpdateDataInTable(Table table)
        {
            var columns = table.Header.ToList();
            var agedTable = new Table(columns.ToArray());

            foreach (var row in table.Rows)
            {
                var rowToAdd = new string[columns.Count];

                for (var column = 0; column < columns.Count; column++)
                {
                    var value = row[column];

                    if (value.Equals(SpecflowNullValue))
                    {
                        value = string.Empty;
                    }
                    else
                    {
                        value = RemoveSingleQuotationMarks(value);
                        value = CalculateDate(value);
                    }

                    rowToAdd[column] = value;
                }

                agedTable.AddRow(rowToAdd);
            }

            return agedTable;
        }
    }
}
