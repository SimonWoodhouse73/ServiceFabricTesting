// ***********************************************************************
// Assembly         : Api
// Author           : MartinG
// Created          : 04-20-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-21-2018
// ***********************************************************************
// <copyright file="TelemetryLabels.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>TelemetryLabels</summary>
// ***********************************************************************

namespace Api.Telemetry
{
    /// <summary>
    /// Label constants for use with Telemetry operations
    /// </summary>
    public static partial class TelemetryLabels
    {
        /// <summary>
        /// For use with calls within GetInsolvencies insolvencyOrders controller method
        /// </summary>
        public const string GetInsolvencyOrders = "Api.GetInsolvencyOrders";

        /// <summary>
        /// For use with calls within GetInsolvency insolvencyOrders controller method
        /// </summary>
        public const string GetInsolvencyOrder = "Api.GetInsolvencyOrder";

        /// <summary>
        /// For use with calls within GetInsolvenciesForResidence insolvencyOrders controller method
        /// </summary>
        public const string GetInsolvencyOrdersForResidence = "Api.GetInsolvencyOrdersForResidence";

        /// <summary>
        /// For use with calls to get a count of ALL Insolvencies in the Insolvencies Repository.
        /// </summary>
        public const string RepositoryCountInsolvencyOrders = "Api.EntityFramework.Repositories.InsolvencyOrdersRepository->Count";

        /// <summary>
        /// For use with calls to get a count of Insolvencies in the Insolvencies Repository by some filter.
        /// </summary>
        public const string RepositoryCountInsolvencyOrdersBy = "Api.EntityFramework.Repositories.InsolvencyOrdersRepository->CountBy";

        /// <summary>
        /// For use with calls to get a page of Insolvencies from the entire collection in the Insolvencies Repository.
        /// </summary>
        public const string RepositoryGetAllInsolvencyOrders = "Api.EntityFramework.Repositories.InsolvencyOrdersRepository->GetAll";

        /// <summary>
        /// For use with calls to get a specific Insolvency by some filter in the Insolvencies Repository.
        /// </summary>
        public const string RepositoryGetInsolvencyOrderBy = "Api.EntityFramework.Repositories.InsolvencyOrdersRepository->GetResultBy";

        /// <summary>
        /// For use with calls to get a set of Insolvencies by some filter in the Insolvencies Repository.
        /// </summary>
        public const string RepositoryGetInsolvencyOrdersBy = "Api.EntityFramework.Repositories.InsolvencyOrdersRepository->GetResultsBy";
    }
}
