﻿using SmartCharging.Domain.Entities;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Interfaces
{
    public interface IChargeStationService
    {
        Task<ChargeStation> Save(ChargeStation chargeStation);
    }
}