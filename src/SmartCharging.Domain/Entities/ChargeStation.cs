using System.Collections.Generic;

namespace SmartCharging.Domain.Entities
{
    public class ChargeStation
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
        public IEnumerable<Connector> Connectors { get; set; }
    }
}
