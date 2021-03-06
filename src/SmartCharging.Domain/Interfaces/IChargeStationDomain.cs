using SmartCharging.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Interfaces
{
    public interface IChargeStationDomain
    {
        Task<ChargeStation> Save(ChargeStation chargeStation);
        Task<ChargeStation> Update(ChargeStation chargeStation);
        Task Delete(Guid groupId, Guid id);
    }
}
