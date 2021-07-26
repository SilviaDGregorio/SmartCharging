namespace SmartCharging.Domain.Entities
{
    public class Connector
    {
        public int Id { get; set; }
        public int ChargeStationId { get; set; }
        public float Amps { get; set; }
        public bool Active { get; set; }
    }
}
