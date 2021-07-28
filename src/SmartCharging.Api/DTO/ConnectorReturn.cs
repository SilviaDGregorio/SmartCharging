using System;

namespace SmartCharging.Api.DTO
{
    public class ConnectorReturn : ConnectorDto
    {
        public int Id { get; set; }
        public Guid ChargeStationId { get; set; }
        public bool Active { get; set; }
    }
}
