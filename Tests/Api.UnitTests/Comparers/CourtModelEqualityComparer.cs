// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-21-2018
//
// Last Modified By : MartinG
// Last Modified On : 03-26-2018
// ***********************************************************************
// <copyright file="CourtModelEqualityComparer.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>CourtModelEqualityComparer class</summary>
// ***********************************************************************
using System.Collections.Generic;
using Callcredit.Domain.Insolvencies.Models;

namespace Api.UnitTests.Comparers
{
    /// <summary>
    /// Implementation of  <see cref="IEqualityComparer{T}"/>
    /// </summary>
    /// <seealso cref="CourtModel" />
    internal class CourtModelEqualityComparer : IEqualityComparer<CourtModel>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type CourtModel to compare.</param>
        /// <param name="y">The second object of type CourtModel to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(CourtModel x, CourtModel y)
        {
            return x.CourtId == y.CourtId &&
                   x.Code.Equals(y.Code) &&
                   x.District.Equals(y.District) &&
                   x.Name.Equals(y.Name);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public int GetHashCode(CourtModel obj)
        {
            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ obj.CourtId;

                var codeNumberHashCode = !string.IsNullOrEmpty(obj.Code) ? obj.Code.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ codeNumberHashCode;

                var districtHashCode = !string.IsNullOrEmpty(obj.District) ? obj.District.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ districtHashCode;

                var nameHashCode = !string.IsNullOrEmpty(obj.Name) ? obj.Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ nameHashCode;

                return hashCode;
            }
        }
    }
}
