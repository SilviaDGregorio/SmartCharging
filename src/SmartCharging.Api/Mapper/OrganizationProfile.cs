using AutoMapper;
using SmartCharging.Api.DTO;
using SmartCharging.Domain.Entities;

namespace SmartCharging.Api.Mapper
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Connector, ConnectorReturn>();
            CreateMap<ConnectorDto, Connector>();

            CreateMap<ChargeStation, ChargeStationReturn>();
            CreateMap<ChargeStationDto, ChargeStation>();

            CreateMap<Group, GroupReturn>();
            CreateMap<GroupDto, Group>();
        }
    }
}
