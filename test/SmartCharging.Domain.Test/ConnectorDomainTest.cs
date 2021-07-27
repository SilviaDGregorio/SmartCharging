using Xunit;
using FluentAssertions;
using NSubstitute;
using SmartCharging.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using SmartCharging.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SmartCharging.Domain.Test
{
    public class ConnectorDomainTest
    {
        private readonly IConnectorService _connectorService;
        private readonly ILogger<ChargeStationDomain> _logger;
        private readonly IChargeStationService _chargeStationService;
        private readonly IGroupService _groupService;
        private readonly ConnectorDomain _connectorDomain;

        public ConnectorDomainTest()
        {
            _chargeStationService = Substitute.For<IChargeStationService>();
            _logger = Substitute.For<ILogger<ChargeStationDomain>>();
            _groupService = Substitute.For<IGroupService>();
            _connectorService = Substitute.For<IConnectorService>();
            _connectorDomain = new ConnectorDomain(_connectorService, _logger, _chargeStationService, _groupService);
        }

        [Fact]
        public async Task Adding_a_connector_On_a_group_without_amps_left_Recieve_an_error()
        {
            //Arrange
            Connector connector = new Connector()
            {
                Amps = 1,
                ChargeStationId = 1
            };

            ChargeStation chargeStation = new ChargeStation()
            {
                Group = new Group()
                {
                    Amps = 1,
                    UsedAmps = 1
                }
            };

            _chargeStationService.GetWithConnectors(connector.ChargeStationId).Returns(chargeStation);

            //Act
            var exception = await Assert.ThrowsAsync<ConnectorsException>(() => _connectorDomain.Save(connector));

            //Assert
            exception.Message.Should().Be("The connector cannot be added because already reach the maximum of amps for the group");
        }

        [Fact]
        public async Task Adding_a_connector_On_a_station_server_with_max_connectors_Recieve_an_error()
        {
            //Arrange
            Connector connector = new Connector()
            {
                Amps = 1,
                ChargeStationId = 1
            };

            ChargeStation chargeStation = new ChargeStation()
            {
                Group = new Group()
                {
                    Amps = 1,
                    UsedAmps = 0
                },
                Connectors = new List<Connector>()
                {
                    new Connector(){ Active = true},
                    new Connector(){ Active = true},
                    new Connector(){ Active = true},
                    new Connector(){ Active = true},
                    new Connector(){ Active = true}
                }
            };

            _chargeStationService.GetWithConnectors(connector.ChargeStationId).Returns(chargeStation);

            //Act
            var exception = await Assert.ThrowsAsync<ConnectorsException>(() => _connectorDomain.Save(connector));

            //Assert
            exception.Message.Should().Be("The connector cannot be added because already reach the maximum of connectors: 5 for the charge station: 1");
        }

        [Fact]
        public async Task Adding_a_connector_On_a_station_server_with_an_inactive_connector_Add_it()
        {
            //Arrange
            Connector connector = new Connector()
            {
                Amps = 1,
                ChargeStationId = 1
            };

            Connector connectorExpected = new Connector()
            {
                Amps = 1,
                ChargeStationId = 1,
                Id = 3,
                Active = true
            };

            ChargeStation chargeStation = new ChargeStation()
            {
                Group = new Group()
                {
                    Amps = 1,
                    UsedAmps = 0
                },
                Connectors = new List<Connector>()
                {
                    new Connector(){ Id = 1, Active = true},
                    new Connector(){ Id = 2, Active = true},
                    new Connector(){ Id = 3, Active = false},
                    new Connector(){ Id = 4, Active = true},
                    new Connector(){ Id = 5, Active = true}
                }
            };

            _chargeStationService.GetWithConnectors(connector.ChargeStationId).Returns(chargeStation);

            //Act
            var response = await _connectorDomain.Save(connector);

            //Assert
            await _groupService.Received().Update(Arg.Is<Group>(x => x.UsedAmps == 1));
            response.Should().BeEquivalentTo(connectorExpected);
        }

        [Fact]
        public async Task Adding_a_connector_On_a_station_server_without_connectors_Add_it()
        {
            //Arrange
            Connector connector = new Connector()
            {
                Amps = 1,
                ChargeStationId = 1
            };

            Connector connectorExpected = new Connector()
            {
                Amps = 1,
                ChargeStationId = 1,
                Id = 1,
                Active = true
            };

            ChargeStation chargeStation = new ChargeStation()
            {
                Group = new Group()
                {
                    Amps = 1,
                    UsedAmps = 0
                }
            };

            _chargeStationService.GetWithConnectors(connector.ChargeStationId).Returns(chargeStation);

            //Act
            var response = await _connectorDomain.Save(connector);

            //Assert
            await _groupService.Received().Update(Arg.Is<Group>(x => x.UsedAmps == 1));
            response.Should().BeEquivalentTo(connectorExpected);
        }
    }
}
