using Xunit;
using FluentAssertions;
using NSubstitute;
using SmartCharging.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using SmartCharging.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace SmartCharging.Domain.Test
{
    public class ConnectorDomainTest
    {
        private readonly IConnectorService _connectorService;
        private readonly ILogger<ChargeStationDomain> _logger;
        private readonly IChargeStationService _chargeStationService;
        private readonly IGroupService _groupService;
        private readonly ConnectorDomain _connectorDomain;
        private readonly Guid _guid;

        public ConnectorDomainTest()
        {
            _guid = Guid.Parse("16995826-f123-46e3-9c6e-e5164ba2b40d");
            _chargeStationService = Substitute.For<IChargeStationService>();
            _logger = Substitute.For<ILogger<ChargeStationDomain>>();
            _groupService = Substitute.For<IGroupService>();
            _connectorService = Substitute.For<IConnectorService>();
            _connectorDomain = new ConnectorDomain(_connectorService, _logger, _chargeStationService, _groupService);
        }

        #region Add
        [Fact]
        public async Task Adding_a_connector_On_a_group_without_amps_left_Recieve_an_error()
        {
            //Arrange
            Connector connector = new Connector()
            {
                Amps = 1,
                ChargeStationId = _guid
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
            var exception = await Assert.ThrowsAsync<AmpsException>(() => _connectorDomain.Save(connector));

            //Assert
            exception.Message.Should().Be("The connector cannot be added/changed because the maximum of amps for the group has already been reached");
        }

        [Fact]
        public async Task Adding_a_connector_On_a_station_server_with_max_connectors_Recieve_an_error()
        {
            //Arrange
            Connector connector = new Connector()
            {
                Amps = 1,
                ChargeStationId = _guid
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
            var exception = await Assert.ThrowsAsync<AmpsException>(() => _connectorDomain.Save(connector));

            //Assert

            exception.Message.Should().Be("The connector cannot be added because the maximum of connectors: 5 for the charge station: 16995826-f123-46e3-9c6e-e5164ba2b40d has already been reached");
        }

        [Fact]
        public async Task Adding_a_connector_On_a_station_server_with_an_inactive_connector_Add_it()
        {
            //Arrange
            Connector connector = new Connector()
            {
                Amps = 1,
                ChargeStationId = _guid
            };

            Connector connectorExpected = new Connector()
            {
                Amps = 1,
                ChargeStationId = _guid,
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
            await _groupService.Received().UpdateUsedAmps(Arg.Is<Group>(x => x.UsedAmps == 1));
            response.Should().BeEquivalentTo(connectorExpected);
        }

        [Fact]
        public async Task Adding_a_connector_On_a_station_server_without_connectors_Add_it()
        {
            //Arrange
            Connector connector = new Connector()
            {
                Amps = 1,
                ChargeStationId = _guid
            };

            Connector connectorExpected = new Connector()
            {
                Amps = 1,
                ChargeStationId = _guid,
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
            await _groupService.Received().UpdateUsedAmps(Arg.Is<Group>(x => x.UsedAmps == 1));
            response.Should().BeEquivalentTo(connectorExpected);
        }
        #endregion
        #region Update

        [Fact]
        public async Task Updating_a_connector_On_a_group_without_amps_left_Recieve_an_error()
        {
            //Arrange
            Connector connector = new Connector()
            {
                Amps = 2,
                ChargeStationId = _guid,
                Id = 5
            };

            ChargeStation chargeStation = new ChargeStation()
            {
                Group = new Group()
                {
                    Amps = 1,
                    UsedAmps = 1
                },
                Connectors = new List<Connector>()
                {
                    new Connector()
                    {
                        Amps = 1,
                        Id = 5
                    }
                }
            };

            _chargeStationService.GetWithConnectors(connector.ChargeStationId).Returns(chargeStation);

            //Act
            var exception = await Assert.ThrowsAsync<AmpsException>(() => _connectorDomain.Update(connector));

            //Assert
            exception.Message.Should().Be("The connector cannot be added/changed because the maximum of amps for the group has already been reached");
        }

        [Fact]
        public async Task Updating_a_connector_to_remove_amps_Updated()
        {
            //Arrange
            Connector connector = new Connector()
            {
                Amps = 1,
                ChargeStationId = _guid,
                Id = 5,
                Active = true
            };

            ChargeStation chargeStation = new ChargeStation()
            {
                Group = new Group()
                {
                    Amps = 2,
                    UsedAmps = 2
                },
                Connectors = new List<Connector>()
                {
                    new Connector()
                    {
                        Amps = 2,
                        Id = 5
                    }
                }
            };

            _chargeStationService.GetWithConnectors(connector.ChargeStationId).Returns(chargeStation);
            _connectorService.Update(connector).Returns(connector);

            //Act
            var response = await _connectorDomain.Update(connector);

            //Assert
            await _groupService.Received().UpdateUsedAmps(Arg.Is<Group>(x => x.UsedAmps == 1));
            response.Should().BeEquivalentTo(connector);
        }

        [Fact]
        public async Task Updating_a_connector_to_add_amps_Group_has_left_Updated()
        {
            //Arrange
            Connector connector = new Connector()
            {
                Amps = 2,
                ChargeStationId = _guid,
                Id = 5,
                Active = true
            };

            ChargeStation chargeStation = new ChargeStation()
            {
                Group = new Group()
                {
                    Amps = 2,
                    UsedAmps = 1
                },
                Connectors = new List<Connector>()
                {
                    new Connector()
                    {
                        Amps = 1,
                        Id = 5
                    }
                }
            };

            _chargeStationService.GetWithConnectors(connector.ChargeStationId).Returns(chargeStation);
            _connectorService.Update(connector).Returns(connector);

            //Act
            var response = await _connectorDomain.Update(connector);

            //Assert
            await _groupService.Received().UpdateUsedAmps(Arg.Is<Group>(x => x.UsedAmps == 2));
            response.Should().BeEquivalentTo(connector);
        }
        #endregion

        #region delete
        [Fact]
        public async Task Deleting_a_connector_Remove_group_usedAmps()
        {
            //Arrange

            ChargeStation chargeStation = new ChargeStation()
            {
                Group = new Group()
                {
                    Amps = 2,
                    UsedAmps = 1
                },
                Connectors = new List<Connector>()
                {
                    new Connector()
                    {
                        Amps = 1,
                        Id = 5
                    }
                }
            };

            _chargeStationService.GetWithConnectors(_guid).Returns(chargeStation);

            //Act
            await _connectorDomain.Delete(_guid, 5);

            //Assert
            await _connectorService.Received().Update(Arg.Is<Connector>(x => x.Active == false && x.Id == 5));
            await _groupService.Received().UpdateUsedAmps(Arg.Is<Group>(x => x.UsedAmps == 0));
        }
        #endregion
    }
}
