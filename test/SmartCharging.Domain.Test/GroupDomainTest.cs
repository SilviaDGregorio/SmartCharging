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
    public class GroupDomainTest
    {

        private readonly ILogger<GroupDomain> _logger;
        private readonly IGroupService _groupService;
        private readonly GroupDomain _groupDomain;

        public GroupDomainTest()
        {

            _logger = Substitute.For<ILogger<GroupDomain>>();
            _groupService = Substitute.For<IGroupService>();

            _groupDomain = new GroupDomain(_groupService, _logger);
        }

        [Fact]
        public async Task Update_group_amps_that_are_use_Get_Error()
        {
            //Arrange
            Group group = new Group()
            {
                Amps = 5,
                Id = 1
            };

            _groupService.GetById(1).Returns(new Group() { Id = 1, UsedAmps = 6, Amps = 7 });


            //Act
            var exception = await Assert.ThrowsAsync<AmpsException>(() => _groupDomain.Update(group));

            //Assert
            exception.Message.Should().Be("The group can not be change, the amps are already in use");
        }
    }
}
