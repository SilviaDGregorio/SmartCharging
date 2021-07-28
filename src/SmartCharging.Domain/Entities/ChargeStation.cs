using System;
using System.Collections.Generic;

namespace SmartCharging.Domain.Entities
{
    public class ChargeStation
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
        public IEnumerable<Connector> Connectors { get; set; }
    }
}
