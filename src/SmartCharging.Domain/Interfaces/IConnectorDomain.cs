using SmartCharging.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Interfaces
{
    public interface IConnectorDomain
    {
        Task<Connector> Save(Connector connector);
        Task<Connector> Update(Connector connectorEntity);
        Task Delete(Guid chargeStationId, int id);
    }
}
