using Microsoft.EntityFrameworkCore;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCharging.DataAccess
{
    public class ConnectorService : IConnectorService
    {
        private readonly SmartCharingDbContext _dbContext;

        public ConnectorService(SmartCharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Connector> Save(Connector connector)
        {
            await _dbContext.Connector.AddAsync(connector);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Connector.FindAsync(connector.Id, connector.ChargeStationId);
        }

        public async Task<Connector> Update(Connector connector)
        {
            var connectorDb = await GetById(connector.Id, connector.ChargeStationId);
            connectorDb.Amps = connector.Amps;
            connectorDb.Active = connector.Active;
            await _dbContext.SaveChangesAsync();
            return connectorDb;
        }

        private async Task<Connector> GetById(int id, int chargeStationId)
        {
            var connectorDb = await _dbContext.Connector.FirstOrDefaultAsync(x => x.Id == id && x.ChargeStationId == chargeStationId);
            if (connectorDb == null)
            {
                throw new KeyNotFoundException($"The connection with id: {id} and charge station: {chargeStationId} does not exist");
            }
            return connectorDb;
        }
    }
}
