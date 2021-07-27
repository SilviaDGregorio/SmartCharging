using SmartCharging.Domain.Entities;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Interfaces
{
    public interface IConnectorService
    {
        Task<Connector> Update(Connector connector);
        Task<Connector> Save(Connector connector);
    }
}
