// ***********************************************************************
// Assembly         : Api
// Author           : markco
// Created          : 05-01-2018
//
// Last Modified By : markco
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="ITelemetryClient.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>ITelemetryClient interface</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace Api.Telemetry
{
    /// <summary>
    /// Interface ITelemetryClient
    /// </summary>
    public interface ITelemetryClient
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        TelemetryContext Context { get; }

        /// <summary>
        /// Gets or sets the instrumentation key.
        /// </summary>
        /// <value>The instrumentation key.</value>
        string InstrumentationKey { get; set; }

        /// <summary>
        /// Flushes this instance.
        /// </summary>
        void Flush();

        /// <summary>
        /// Initializes the specified telemetry.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        void Initialize(ITelemetry telemetry);

        /// <summary>
        /// Determines whether this instance is enabled.
        /// </summary>
        /// <returns><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</returns>
        bool IsEnabled();

        /// <summary>
        /// Tracks the specified telemetry.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        void Track(ITelemetry telemetry);

        /// <summary>
        /// Tracks the availability.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        void TrackAvailability(AvailabilityTelemetry telemetry);

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
        void TrackAvailability(string name, DateTimeOffset timeStamp, TimeSpan duration, string runLocation, bool success, string message = null, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);

        /// <summary>
        /// Tracks the dependency.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        void TrackDependency(DependencyTelemetry telemetry);

        /// <summary>
        /// Tracks the dependency.
        /// </summary>
        /// <param name="dependencyName">Name of the dependency.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration, bool success);

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
        void TrackDependency(string dependencyTypeName, string target, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, string resultCode, bool success);

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        void TrackEvent(EventTelemetry telemetry);

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="metrics">The metrics.</param>
        void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);

        /// <summary>
        /// Tracks the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="metrics">The metrics.</param>
        void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);

        /// <summary>
        /// Tracks the exception.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        void TrackException(ExceptionTelemetry telemetry);

        /// <summary>
        /// Tracks the metric.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        void TrackMetric(MetricTelemetry telemetry);

        /// <summary>
        /// Tracks the metric.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="properties">The properties.</param>
        void TrackMetric(string name, double value, IDictionary<string, string> properties = null);

        /// <summary>
        /// Tracks the page view.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        void TrackPageView(PageViewTelemetry telemetry);

        /// <summary>
        /// Tracks the page view.
        /// </summary>
        /// <param name="name">The name.</param>
        void TrackPageView(string name);

        /// <summary>
        /// Tracks the request.
        /// </summary>
        /// <param name="request">The request.</param>
        void TrackRequest(RequestTelemetry request);

        /// <summary>
        /// Tracks the request.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="responseCode">The response code.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        void TrackRequest(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success);

        /// <summary>
        /// Tracks the trace.
        /// </summary>
        /// <param name="message">The message.</param>
        void TrackTrace(string message);

        /// <summary>
        /// Tracks the trace.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="properties">The properties.</param>
        void TrackTrace(string message, IDictionary<string, string> properties);

        /// <summary>
        /// Tracks the trace.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severityLevel">The severity level.</param>
        void TrackTrace(string message, SeverityLevel severityLevel);

        /// <summary>
        /// Tracks the trace.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severityLevel">The severity level.</param>
        /// <param name="properties">The properties.</param>
        void TrackTrace(string message, SeverityLevel severityLevel, IDictionary<string, string> properties);

        /// <summary>
        /// Tracks the trace.
        /// </summary>
        /// <param name="telemetry">The telemetry.</param>
        void TrackTrace(TraceTelemetry telemetry);

        /// <summary>
        /// Starts the operation.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="operationName">Name of the operation.</param>
        /// <returns>IOperationHolder{T}.</returns>
        IOperationHolder<T> StartOperation<T>(string operationName)
            where T : OperationTelemetry, new();

        /// <summary>
        /// Starts the operation.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="operationId">The operation identifier.</param>
        /// <param name="parentOperationId">The parent operation identifier.</param>
        /// <returns>IOperationHolder{T}.</returns>
        IOperationHolder<T> StartOperation<T>(string operationName, string operationId, string parentOperationId = null)
            where T : OperationTelemetry, new();

        /// <summary>
        /// Starts the operation.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="operationTelemetry">The operation telemetry.</param>
        /// <returns>IOperationHolder&lt;T&gt;.</returns>
        IOperationHolder<T> StartOperation<T>(T operationTelemetry)
            where T : OperationTelemetry;

        /// <summary>
        /// Stops the operation.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="operation">The operation.</param>
        void StopOperation<T>(IOperationHolder<T> operation)
            where T : OperationTelemetry;
    }
}
