using System;

namespace SmartCharging.Domain.Entities
{
    public class Connector
    {
        public int Id { get; set; }
        public Guid ChargeStationId { get; set; }
        public float Amps { get; set; }
        public bool Active { get; set; } = true;
    }
}
