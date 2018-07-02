// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 03-21-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="DisputeModelEqualityComparer.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>DisputeModelEqualityComparer class</summary>
// ***********************************************************************
using System.Collections.Generic;
using Callcredit.Domain.Insolvencies.Models;

namespace Api.UnitTests.Comparers
{
    /// <summary>
    /// Implementation of  <see cref="IEqualityComparer{T}"/>
    /// </summary>
    /// <seealso cref="DisputeModel" />
    internal class DisputeModelEqualityComparer : IEqualityComparer<DisputeModel>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type DisputeModel to compare.</param>
        /// <param name="y">The second object of type DisputeModel to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(DisputeModel x, DisputeModel y)
        {
            return x.DisputeId == y.DisputeId &&
                   x.InsolvencyOrderId == y.InsolvencyOrderId &&
                   x.DateRaised.Value == y.DateRaised.Value &&
                   x.Displayed == y.Displayed &&
                   x.ReferenceNumber.Equals(y.ReferenceNumber);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public int GetHashCode(DisputeModel obj)
        {
            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ obj.DisputeId;
                hashCode = (hashCode * 397) ^ obj.InsolvencyOrderId.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.DateRaised.Value.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.Displayed.Value.GetHashCode();

                var referenceNumberHashCode = !string.IsNullOrEmpty(obj.ReferenceNumber) ? obj.ReferenceNumber.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ referenceNumberHashCode;

                return hashCode;
            }
        }
    }
}
