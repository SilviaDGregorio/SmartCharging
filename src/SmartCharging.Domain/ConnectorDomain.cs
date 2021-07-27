using Microsoft.Extensions.Logging;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
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
            chargeStation.Group.UsedAmps = CalculateAmps(connector, chargeStation);
            int numberOfConnectors = chargeStation?.Connectors?.Count() ?? 0;

            if (numberOfConnectors == NumberOfConnectors)
            {
                var connectorInactive = GetInactiveConnector(connector, chargeStation);
                connector.Id = connectorInactive.Id;
                connector.Active = true;
                await _connectorService.Update(connector);
            }
            else
            {
                connector.Id = numberOfConnectors + 1;
                connector.Active = true;
                await _connectorService.Save(connector);
            }

            await _groupService.Update(chargeStation.Group);
            return connector;
        }

        private Connector GetInactiveConnector(Connector connector, ChargeStation chargeStation)
        {
            var connectorInactive = chargeStation.Connectors.FirstOrDefault(x => x.Active == false);
            if (connectorInactive == null)
            {
                string message = $"The connector cannot be added because already reach the maximum of connectors: {NumberOfConnectors} for the charge station: {connector.ChargeStationId}";
                _logger.LogError($"The connector cannot be added because already reach the maximum of connectors: {NumberOfConnectors} for the charge station: {connector.ChargeStationId}");
                throw new ConnectorsException(message);
            }
            return connectorInactive;
        }

        private float CalculateAmps(Connector connector, ChargeStation chargeStation)
        {
            var usedAmps = chargeStation.Group.UsedAmps + connector.Amps;
            if (usedAmps > chargeStation.Group.Amps)
            {
                string message = "The connector cannot be added because already reach the maximum of amps for the group";
                _logger.LogError("The connector cannot be added because already reach the maximum of amps for the group");
                throw new ConnectorsException(message);
            }
            return usedAmps;
        }
    }
}
