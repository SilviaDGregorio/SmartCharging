using SmartCharging.Domain.Entities;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Interfaces
{
    public interface IConnectorDomain
    {
        Task<Connector> Save(Connector connector);
        Task<Connector> Update(Connector connectorEntity);
        Task Delete(int chargeStationId, int id);
    }
}
