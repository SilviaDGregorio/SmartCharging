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
    public class GroupDomainTest
    {

        private readonly ILogger<GroupDomain> _logger;
        private readonly IGroupService _groupService;
        private readonly GroupDomain _groupDomain;
        private readonly Guid _guid;

        public GroupDomainTest()
        {

            _logger = Substitute.For<ILogger<GroupDomain>>();
            _groupService = Substitute.For<IGroupService>();
            _guid = Guid.Parse("16995826-f123-46e3-9c6e-e5164ba2b40d");
            _groupDomain = new GroupDomain(_groupService, _logger);
        }

        [Fact]
        public async Task Update_group_amps_that_are_use_Get_Error()
        {
            //Arrange
            Group group = new Group()
            {
                Amps = 5,
                Id = _guid
            };

            _groupService.GetById(_guid).Returns(new Group() { Id = _guid, UsedAmps = 6, Amps = 7 });


            //Act
            var exception = await Assert.ThrowsAsync<AmpsException>(() => _groupDomain.Update(group));

            //Assert
            exception.Message.Should().Be("The group can not be change, the amps are already in use");
        }
    }
}
