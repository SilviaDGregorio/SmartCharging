using SmartCharging.Domain.Entities;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Interfaces
{
    public interface IChargeStationService
    {
        Task<ChargeStation> Save(ChargeStation chargeStation);
        Task<ChargeStation> Update(ChargeStation chargeStation);
        Task Delete(int groupId, int id);
        Task<ChargeStation> GetWithConnectors(int id);
    }
}
