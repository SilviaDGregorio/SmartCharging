using System;

namespace SmartCharging.Api.DTO
{
    public class ChargeStationReturn : ChargeStationDto
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
    }
}
