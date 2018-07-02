// ***********************************************************************
// Assembly         : Api
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 04-20-2018
// ***********************************************************************
// <copyright file="TelemetryClientWrapper.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>TelemetryClientWrapper</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace Api.Telemetry
{
    /// <summary>
    /// Class TelemetryClientWrapper.
    /// </summary>
    /// <seealso cref="Api.Telemetry.ITelemetryClient" />
    public class TelemetryClientWrapper : ITelemetryClient
    {
        /// <summary>
        /// The telemetry client
        /// </summary>
        private readonly TelemetryClient telemetryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetryClientWrapper" /> class.
        /// </summary>
        public TelemetryClientWrapper()
        {
            telemetryClient = new TelemetryClient();
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public TelemetryContext Context => telemetryClient.Context;

        /// <summary>
        /// Gets or sets the instrumentation key.
        /// </summary>
        /// <value>The instrumentation key.</value>
        public string InstrumentationKey
        {
            get => telemetryClient.InstrumentationKey;
            set => telemetryClient.InstrumentationKey = value;
        }

        /// <summary>
        /// Flushes this instance.
        /// </summary>
        public void Flush()
        {
            telemetryClient.Flush();
        }

        /// <summary>
        /// Initializes the specified telemetry.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        public void Initialize(ITelemetry telemetry)
        {
            telemetryClient.Initialize(telemetry);
        }

        /// <summary>
        /// Determines whether this instance is enabled.
        /// </summary>
        /// <returns><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</returns>
        public bool IsEnabled()
        {
            return telemetryClient.IsEnabled();
        }

        /// <summary>
        /// Starts the operation.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="operationName">Name of the operation.</param>
        /// <returns>IOperationHolder{T}.</returns>
        public IOperationHolder<T> StartOperation<T>(string operationName)
            where T : OperationTelemetry, new()
        {
            return telemetryClient.StartOperation<T>(operationName);
        }

        /// <summary>
        /// Starts the operation.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="operationId">The operation identifier.</param>
        /// <param name="parentOperationId">The parent operation identifier.</param>
        /// <returns>IOperationHolder{T}.</returns>
        public IOperationHolder<T> StartOperation<T>(string operationName, string operationId, string parentOperationId = null)
            where T : OperationTelemetry, new()
        {
            return telemetryClient.StartOperation<T>(operationName, operationId, parentOperationId);
        }

        /// <summary>
        /// Starts the operation.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="operationTelemetry">The operation telemetry.</param>
        /// <returns>IOperationHolder&lt;T&gt;.</returns>
        public IOperationHolder<T> StartOperation<T>(T operationTelemetry)
            where T : OperationTelemetry
        {
            return telemetryClient.StartOperation<T>(operationTelemetry);
        }

        /// <summary>
        /// Stops the operation.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="operation">The operation.</param>
        public void StopOperation<T>(IOperationHolder<T> operation)
            where T : OperationTelemetry
        {
            telemetryClient.StopOperation<T>(operation);
        }

        /// <summary>
        /// Tracks the specified telemetry.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        public void Track(ITelemetry telemetry)
        {
            telemetryClient.Track(telemetry);
        }

        /// <summary>
        /// Tracks the availability.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        public void TrackAvailability(AvailabilityTelemetry telemetry)
        {
            telemetryClient.TrackAvailability(telemetry);
        }

        /// <summary>
        /// Tracks the availability.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="runLocation">The run location.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="message">The message.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="metrics">The metrics.</param>
        public void TrackAvailability(string name, DateTimeOffset timeStamp, TimeSpan duration, string runLocation, bool success, string message = null, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            telemetryClient.TrackAvailability(name, timeStamp, duration, runLocation, success, message, properties, metrics);
        }

        /// <summary>
        /// Tracks the dependency.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        public void TrackDependency(DependencyTelemetry telemetry)
        {
            telemetryClient.TrackDependency(telemetry);
        }

        /// <summary>
        /// Tracks the dependency.
        /// </summary>
        /// <param name="dependencyName">Name of the dependency.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        public void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            telemetryClient.TrackDependency(dependencyName, commandName, startTime, duration, success);
        }

        /// <summary>
        /// Tracks the dependency.
        /// </summary>
        /// <param name="dependencyTypeName">Name of the dependency type.</param>
        /// <param name="target">The target.</param>
        /// <param name="dependencyName">Name of the dependency.</param>
        /// <param name="data">The data.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="resultCode">The result code.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        public void TrackDependency(string dependencyTypeName, string target, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, string resultCode, bool success)
        {
            telemetryClient.TrackDependency(dependencyTypeName, target, dependencyName, data, startTime, duration, resultCode, success);
        }

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        public void TrackEvent(EventTelemetry telemetry)
        {
            telemetryClient.TrackEvent(telemetry);
        }

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="metrics">The metrics.</param>
        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            telemetryClient.TrackEvent(eventName, properties, metrics);
        }

        /// <summary>
        /// Tracks the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="metrics">The metrics.</param>
        public void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            telemetryClient.TrackException(exception, properties, metrics);
        }

        /// <summary>
        /// Tracks the exception.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        public void TrackException(ExceptionTelemetry telemetry)
        {
            telemetryClient.TrackException(telemetry);
        }

        /// <summary>
        /// Tracks the metric.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        public void TrackMetric(MetricTelemetry telemetry)
        {
            telemetryClient.TrackMetric(telemetry);
        }

        /// <summary>
        /// Tracks the metric.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="properties">The properties.</param>
        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            telemetryClient.TrackMetric(name, value, properties);
        }

        /// <summary>
        /// Tracks the page view.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        public void TrackPageView(PageViewTelemetry telemetry)
        {
            telemetryClient.TrackPageView(telemetry);
        }

        /// <summary>
        /// Tracks the page view.
        /// </summary>
        /// <param name="name">The name.</param>
        public void TrackPageView(string name)
        {
            telemetryClient.TrackPageView(name);
        }

        /// <summary>
        /// Tracks the request.
        /// </summary>
        /// <param name="request">The request.</param>
        public void TrackRequest(RequestTelemetry request)
        {
            telemetryClient.TrackRequest(request);
        }

        /// <summary>
        /// Tracks the request.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="responseCode">The response code.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        public void TrackRequest(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
        {
            telemetryClient.TrackRequest(name, startTime, duration, responseCode, success);
        }

        /// <summary>
        /// Tracks the trace.
        /// </summary>
        /// <param name="message">The message.</param>
        public void TrackTrace(string message)
        {
            telemetryClient.TrackTrace(message);
        }

        /// <summary>
        /// Tracks the trace.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="properties">The properties.</param>
        public void TrackTrace(string message, IDictionary<string, string> properties)
        {
            telemetryClient.TrackTrace(message, properties);
        }

        /// <summary>
        /// Tracks the trace.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severityLevel">The severity level.</param>
        public void TrackTrace(string message, SeverityLevel severityLevel)
        {
            telemetryClient.TrackTrace(message, severityLevel);
        }

        /// <summary>
        /// Tracks the trace.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severityLevel">The severity level.</param>
        /// <param name="properties">The properties.</param>
        public void TrackTrace(string message, SeverityLevel severityLevel, IDictionary<string, string> properties)
        {
            telemetryClient.TrackTrace(message, severityLevel, properties);
        }

        /// <summary>
        /// Tracks the trace.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        public void TrackTrace(TraceTelemetry telemetry)
        {
            telemetryClient.TrackTrace(telemetry);
        }
    }
}
