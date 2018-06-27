using System;
using System.Linq;
using Api.EntityFramework.Entities;
using AutoMapper;
using Callcredit.Domain.Insolvencies.Models;

namespace Api.EntityFramework.Configuration
{
    /// <summary>
    /// Class ModelMappingConfiguration.
    /// </summary>
    public static class ModelMappingConfiguration
    {
        /// <summary>
        /// Configures the AutoMapper model mapping rules.
        /// </summary>
        public static void CreateModelMapping()
        {
            Mapper.Initialize(configuration =>
            {
                configuration.AllowNullCollections = false;
                configuration.AllowNullDestinationValues = true;

                configuration.CreateMap<InsolvencyOrderTypeEntity, InsolvencyOrderTypeModel>()
                    .ForMember(model => model.Code, memberConfig => memberConfig.MapFrom(entity => entity.CallReportCode))
                    .ForMember(model => model.Description, memberConfig => memberConfig.MapFrom(entity => entity.Description))
                    .ForMember(model => model.InsolvencyOrderTypeId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderTypeId));

                configuration.CreateMap<InsolvencyOrderPersonEntity, InsolvencyOrderPersonModel>()
                    .ForMember(model => model.InsolvencyOrderPersonId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderPersonId))
                    .ForMember(model => model.Title, memberConfig => memberConfig.MapFrom(entity => entity.Title))
                    .ForMember(model => model.Forename, memberConfig => memberConfig.MapFrom(entity => entity.Forename))
                    .ForMember(model => model.Surname, memberConfig => memberConfig.MapFrom(entity => entity.Surname))
                    .ForMember(model => model.DateOfBirth, memberConfig => memberConfig.MapFrom(entity => entity.DateOfBirth));

                configuration.CreateMap<InsolvencyOrderAddressEntity, InsolvencyOrderAddressModel>()
                    .ForMember(model => model.InsolvencyOrderId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderId))
                    .ForMember(model => model.InsolvencyOrderAddressId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderAddressId))
                    .ForMember(model => model.Address, memberConfig => memberConfig.MapFrom(entity => entity.LastKnownAddress))
                    .ForMember(model => model.PostCode, memberConfig => memberConfig.MapFrom(entity => entity.LastKnownPostCode));

                configuration.CreateMap<CourtEntity, CourtModel>()
                    .ForMember(model => model.CourtId, memberConfig => memberConfig.MapFrom(entity => entity.CourtId))
                    .ForMember(model => model.Name, memberConfig => memberConfig.MapFrom(entity => entity.CourtName))
                    .ForMember(model => model.Code, memberConfig => memberConfig.MapFrom(entity => entity.CourtCode))
                    .ForMember(model => model.District, memberConfig => memberConfig.MapFrom(entity => entity.District));

                configuration.CreateMap<InsolvencyOrderHistoryEntity, InsolvencyOrderHistoryModel>()
                    .ForMember(model => model.InsolvencyOrderHistoryId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderHistoryId))
                    .ForMember(model => model.InsolvencyOrderId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderId))
                    .ForMember(model => model.InsolvencyOrderStatusId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderStatusId))
                    .ForMember(model => model.CourtId, memberConfig => memberConfig.MapFrom(entity => entity.CourtId))
                    .ForMember(model => model.CaseReference, memberConfig => memberConfig.MapFrom(entity => entity.CaseReference))
                    .ForMember(model => model.CaseYear, memberConfig => memberConfig.MapFrom(entity => entity.CaseYear));

                configuration.CreateMap<InsolvencyRestrictionsTypeEntity, InsolvencyOrderRestrictionsTypeModel>()
                    .ForMember(model => model.RestrictionsTypeId, memberConfig => memberConfig.MapFrom(entity => entity.RestrictionsTypeId))
                    .ForMember(model => model.Code, memberConfig => memberConfig.MapFrom(entity => entity.Code))
                    .ForMember(model => model.Description, memberConfig => memberConfig.MapFrom(entity => entity.Description));

                configuration.CreateMap<InsolvencyTradingDetailsEntity, InsolvencyOrderTradingDetailsModel>()
                    .ForMember(model => model.InsolvencyOrderTradingDetailsId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyTradingId))
                    .ForMember(model => model.InsolvencyOrderId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderId))
                    .ForMember(model => model.Address, memberConfig => memberConfig.MapFrom(entity => entity.TradingAddress))
                    .ForMember(model => model.Name, memberConfig => memberConfig.MapFrom(entity => entity.TradingName));

                configuration.CreateMap<InsolvencyOrderStatusEntity, InsolvencyOrderStatusModel>()
                    .ForMember(model => model.InsolvencyOrderStatusId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderStatusId))
                    .ForMember(model => model.Code, memberConfig => memberConfig.MapFrom(entity => entity.StatusAggregate))
                    .ForMember(model => model.Description, memberConfig => memberConfig.MapFrom(entity => entity.Description));

                configuration.CreateMap<DisputeEntity, DisputeModel>()
                    .ForMember(model => model.ReferenceNumber, memberConfig => memberConfig.MapFrom(entity => entity.RefNum))
                    .ForMember(model => model.DateRaised, memberConfig => memberConfig.MapFrom(entity => entity.DateRaised))
                    .ForMember(model => model.Displayed, memberConfig => memberConfig.MapFrom(entity => entity.Displayed))
                    .ForMember(model => model.DisputeId, memberConfig => memberConfig.MapFrom(entity => entity.DisputeId))
                    .ForMember(
                        model => model.InsolvencyOrderId,
                        memberConfig =>
                            memberConfig.MapFrom(
                                entity =>
                                    entity.InsolvencyOrderDisputes
                                    .Where(i => i.DisputeId == entity.DisputeId)
                                    .Select(i => i.InsolvencyOrderId)
                                    .FirstOrDefault()));

                configuration.CreateMap<InsolvencyOrderDisputeEntity, DisputeModel>()
                    .ForMember(model => model.DateRaised, memberConfig => memberConfig.MapFrom(entity => entity.Dispute.DateRaised))
                    .ForMember(model => model.Displayed, memberConfig => memberConfig.MapFrom(entity => entity.Dispute.Displayed))
                    .ForMember(model => model.DisputeId, memberConfig => memberConfig.MapFrom(entity => entity.DisputeId))
                    .ForMember(model => model.InsolvencyOrderId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderId))
                    .ForMember(model => model.ReferenceNumber, memberConfig => memberConfig.MapFrom(entity => entity.Dispute.RefNum));

                configuration.CreateMap<InsolvencyOrderEntity, InsolvencyOrderModel>()
                    .ForMember(model => model.InsolvencyOrderId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderId))
                    .ForMember(model => model.InsolvencyOrderTypeId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderTypeId))
                    .ForMember(model => model.InsolvencyOrderType, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderType))
                    .ForMember(model => model.InsolvencyServiceCaseId, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyServiceCaseId))
                    .ForMember(model => model.ResidenceId, memberConfig => memberConfig.MapFrom(entity => entity.ResidenceId))
                    .ForMember(model => model.OrderDate, memberConfig => memberConfig.MapFrom(entity => entity.OrderDate))
                    .ForMember(model => model.RestrictionsTypeId, memberConfig => memberConfig.MapFrom(entity => entity.RestrictionsTypeId))
                    .ForMember(model => model.RestrictionsType, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyRestrictionsType))
                    .ForMember(model => model.RestrictionsStartDate, memberConfig => memberConfig.MapFrom(entity => entity.RestrictionsStartDate))
                    .ForMember(model => model.RestrictionsEndDate, memberConfig => memberConfig.MapFrom(entity => entity.RestrictionsEndDate))
                    .ForMember(model => model.LineOfBusiness, memberConfig => memberConfig.MapFrom(entity => entity.LineOfBusiness))
                    .ForMember(model => model.ValueOfDebt, memberConfig => memberConfig.MapFrom(entity => entity.ValueOfDebt))
                    .ForMember(model => model.DischargeDate, memberConfig => memberConfig.MapFrom(entity => entity.DischargeDate))
                    .ForMember(model => model.InsolvencyOrderPersons, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderPersons))
                    .ForMember(model => model.InsolvencyOrderAddresses, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderAddresses))
                    .ForMember(model => model.InsolvencyOrderHistories, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderHistory))
                    .ForMember(model => model.InsolvencyOrderTradingDetails, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyTradingDetails))
                    .ForMember(model => model.Disputes, memberConfig => memberConfig.MapFrom(entity => entity.InsolvencyOrderDisputes));
            });
        }
    }
}
