using Microsoft.Extensions.Logging;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace SmartCharging.Domain
{
    public class ChargeStationDomain : IChargeStationDomain
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<ChargeStationDomain> _loggs;
        private readonly IChargeStationService _chargeStationService;

        public ChargeStationDomain(IGroupService groupService, IChargeStationService chargeStationService, ILogger<ChargeStationDomain> loggs)
        {
            _loggs = loggs;
            _groupService = groupService;
            _chargeStationService = chargeStationService;
        }

        public async Task<ChargeStation> Save(ChargeStation chargeStation)
        {
            try
            {
                _ = await _groupService.GetById(chargeStation.GroupId);
                return await _chargeStationService.Save(chargeStation);
            }
            catch (Exception ex)
            {
                _loggs.LogError(ex, "Something when wrong trying to add a new charge station");
                throw;
            }

        }

        public async Task<ChargeStation> Update(ChargeStation chargeStation)
        {
            try
            {
                return await _chargeStationService.Update(chargeStation);
            }
            catch (Exception ex)
            {
                _loggs.LogError(ex, "Something when wrong trying to update a new charge station");
                throw;
            }
        }
    }
}
