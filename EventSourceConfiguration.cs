// ***********************************************************************
// Assembly         : Api
// Author           : markco
// Created          : 03-01-2018
//
// Last Modified By : markco
// Last Modified On : 03-02-2018
// ***********************************************************************
// <copyright file="EventSourceConfiguration.cs" company="Callcredit Information Group.">
//     Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>EventSourceConfiguration class</summary>
// ***********************************************************************
namespace Api
{
    /// <summary>
    /// Class EventSourceConfiguration.
    /// </summary>
    public class EventSourceConfiguration
    {
        /// <summary>
        /// Gets or sets the rest service event source.
        /// </summary>
        /// <value>The rest service event source.</value>
        public string RESTServiceEventSource { get; set; }

        /// <summary>
        /// Gets or sets the data asset event source.
        /// </summary>
        /// <value>The data asset event source.</value>
        public string DataAssetEventSource { get; set; }

        /// <summary>
        /// Gets or sets the platform event source.
        /// </summary>
        /// <value>The platform event source.</value>
        public string PlatformEventSource { get; set; }

        /// <summary>
        /// Gets or sets the database context event source.
        /// </summary>
        /// <value>The database context event source.</value>
        public string DatabaseContextEventSource { get; set; }
    }
}
