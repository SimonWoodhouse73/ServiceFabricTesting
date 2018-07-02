// ***********************************************************************
// Assembly         : Api
// Author           : markco
// Created          : 03-01-2018
//
// Last Modified By : markco
// Last Modified On : 05-22-2018
// ***********************************************************************
// <copyright file="IApiEventSource.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>IApiEventSource interface</summary>
// ***********************************************************************
namespace Api.Logging
{
    /// <summary>
    /// Interface IApiEventSource
    /// </summary>
    public interface IApiEventSource
    {
        /// <summary>
        /// Executions the time.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="elapsedTime">The elapsed time.</param>
        void ExecutionTime(string operation, long elapsedTime);
    }
}
