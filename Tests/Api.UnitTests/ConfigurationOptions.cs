// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 04-20-2018
// ***********************************************************************
// <copyright file="ConfigurationOption.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>ConfigurationOption</summary>
// ***********************************************************************
using Callcredit.Domain.Repositories.GDPR;
using Microsoft.Extensions.Options;

namespace Api.UnitTests
{
    /// <summary>
    /// Class ConfigurationOption.
    /// </summary>
    /// <seealso cref="IOptions{RetentionOptions}" />
    public class ConfigurationOption : IOptions<RetentionOptions>
    {
        /// <summary>
        /// The value
        /// </summary>
        private readonly RetentionOptions value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationOption"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ConfigurationOption(RetentionOptions value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets the IOptions of RetentionOptions value.
        /// </summary>
        /// <value>The value.</value>
        RetentionOptions IOptions<RetentionOptions>.Value => value;
    }
}
