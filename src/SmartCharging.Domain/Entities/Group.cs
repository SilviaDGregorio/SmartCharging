using System.Collections.Generic;

namespace SmartCharging.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amps { get; set; }
        public IEnumerable<ChargeStation> ChargeStations { get; set; }
        public float UsedAmps { get; set; }

    }
}
