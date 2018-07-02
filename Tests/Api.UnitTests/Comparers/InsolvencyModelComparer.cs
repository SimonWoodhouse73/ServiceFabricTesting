// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-26-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="InsolvencyModelComparer.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>InsolvencyModelComparer class</summary>
// ***********************************************************************
using System.Collections;
using Callcredit.Domain.Insolvencies.Models;

namespace Api.UnitTests.Comparers
{
    /// <summary>
    /// Implementation of  <see cref="IComparer"/>
    /// </summary>
    internal class InsolvencyModelComparer : IComparer
    {
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero <paramref name="x" /> is less than <paramref name="y" />. Zero <paramref name="x" /> equals <paramref name="y" />. Greater than zero <paramref name="x" /> is greater than <paramref name="y" />.</returns>
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            var first = x as InsolvencyOrderModel;
            var second = y as InsolvencyOrderModel;

            return first.InsolvencyOrderId.CompareTo(second.InsolvencyOrderId);
        }
    }
}
