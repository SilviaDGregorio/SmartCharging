using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
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
    }
}
