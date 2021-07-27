using Microsoft.Extensions.Logging;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCharging.Domain
{
    public class ConnectorDomain : IConnectorDomain
    {
        private const int NumberOfConnectors = 5; // I would have it on the settings, but for now I add it like a const
        private readonly IConnectorService _connectorService;
        private readonly ILogger<ChargeStationDomain> _logger;
        private readonly IChargeStationService _chargeStationService;
        private readonly IGroupService _groupService;

        public ConnectorDomain(IConnectorService connectorService, ILogger<ChargeStationDomain> logger, IChargeStationService chargeStationService, IGroupService groupService)
        {
            _connectorService = connectorService;
            _logger = logger;
            _chargeStationService = chargeStationService;
            _groupService = groupService;
        }

        public async Task<Connector> Save(Connector connector)
        {
            var chargeStation = await _chargeStationService.GetWithConnectors(connector.ChargeStationId);
            chargeStation.Group.UsedAmps = CalculateAmps(connector.Amps, chargeStation);
            int numberOfConnectors = chargeStation?.Connectors?.Count() ?? 0;

            if (numberOfConnectors == NumberOfConnectors)
            {
                var connectorInactive = GetInactiveConnector(connector, chargeStation);
                connector.Id = connectorInactive.Id;
                await _connectorService.Update(connector);
            }
            else
            {
                connector.Id = numberOfConnectors + 1;
                await _connectorService.Save(connector);
            }

            await _groupService.Update(chargeStation.Group);
            return connector;
        }

        public async Task<Connector> Update(Connector connector)
        {
            try
            {
                var chargeStation = await _chargeStationService.GetWithConnectors(connector.ChargeStationId);
                var connectorDb = GetConnector(connector, chargeStation);
                var difference = connectorDb.Amps - connector.Amps;

                if (difference > 0)
                {
                    chargeStation.Group.UsedAmps -= difference;
                }
                else
                {
                    chargeStation.Group.UsedAmps = CalculateAmps(Math.Abs(difference), chargeStation);
                }

                connectorDb = await _connectorService.Update(connector);
                await _groupService.Update(chargeStation.Group);
                return connectorDb;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something when wrong trying to update a new connector");
                throw;
            }
        }

        private Connector GetInactiveConnector(Connector connector, ChargeStation chargeStation)
        {
            var connectorInactive = chargeStation.Connectors.FirstOrDefault(x => x.Active == false);
            if (connectorInactive == null)
            {
                string message = $"The connector cannot be added because already reach the maximum of connectors: {NumberOfConnectors} for the charge station: {connector.ChargeStationId}";
                _logger.LogError(message);
                throw new ConnectorsException(message);
            }
            return connectorInactive;
        }

        private float CalculateAmps(float ampsToAdd, ChargeStation chargeStation)
        {
            var usedAmps = chargeStation.Group.UsedAmps + ampsToAdd;
            if (usedAmps > chargeStation.Group.Amps)
            {
                string message = "The connector cannot be added/changed because already reach the maximum of amps for the group";
                _logger.LogError(message);
                throw new ConnectorsException(message);
            }
            return usedAmps;
        }

        private Connector GetConnector(Connector connector, ChargeStation chargeStation)
        {
            var connectorDB = chargeStation.Connectors.First(x => x.Id == connector.Id);
            if (connectorDB == null)
            {
                string message = $"The connector with id: {connector.Id} and charge station: {chargeStation.Id} does not exist";
                _logger.LogError(message);
                throw new KeyNotFoundException(message);
            }
            return connectorDB;
        }
    }
}
