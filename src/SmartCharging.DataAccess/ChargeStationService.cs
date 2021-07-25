using Microsoft.EntityFrameworkCore;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCharging.DataAccess
{
    public class ChargeStationService : IChargeStationService
    {
        private readonly SmartCharingDbContext _dbContext;

        public ChargeStationService(SmartCharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ChargeStation> Save(ChargeStation chargeStation)
        {
            await _dbContext.ChargeStation.AddAsync(chargeStation);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.ChargeStation.FindAsync(chargeStation.Id);
        }

        public async Task<ChargeStation> Update(ChargeStation chargeStation)
        {
            var chargeStationDb = await GetById(chargeStation.Id, chargeStation.GroupId);
            chargeStationDb.Name = chargeStation.Name;
            await _dbContext.SaveChangesAsync();
            return chargeStationDb;
        }

        public async Task<ChargeStation> GetById(int id, int chargeId)
        {
            var stationDb = await _dbContext.ChargeStation.FirstOrDefaultAsync(x => x.Id == id && x.GroupId == chargeId);
            if (stationDb == null)
            {
                throw new KeyNotFoundException($"The charge station with id: {id} and groupId: {chargeId} does not exist");
            }
            return stationDb;
        }
    }
}
