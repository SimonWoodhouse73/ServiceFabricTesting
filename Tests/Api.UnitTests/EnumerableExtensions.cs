// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-02-2018
//
// Last Modified By : MartinG
// Last Modified On : 03-02-2018
// ***********************************************************************
// <copyright file="EnumerableExtensions.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>Defines an extension to IEnumberable that takes Page information into account
// when using the Take method.</summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using Callcredit.Domain.Repositories;

namespace Api.UnitTests
{
    /// <summary>
    /// Class EnumerableExtensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Pages the specified page information.
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="pageInformation">The page information.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> data, PageInformation pageInformation) =>
            data.Skip((pageInformation.Page - 1) * pageInformation.PageSize).Take(pageInformation.PageSize);
    }
}
