using Microsoft.Extensions.Logging;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace SmartCharging.Domain
{
    public class GroupDomain : IGroupDomain
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupDomain> _loggs;

        public GroupDomain(IGroupService groupService, ILogger<GroupDomain> loggs)
        {
            _loggs = loggs;
            _groupService = groupService;
        }

        public async Task<Group> Save(Group group)
        {
            try
            {
                return await _groupService.Save(group);
            }
            catch (Exception ex)
            {
                _loggs.LogError(ex, "Something when wrong trying to add a new group");
                throw;
            }
        }

        public async Task<Group> Update(Group group)
        {
            try
            {
                return await _groupService.Update(group);
            }
            catch (Exception ex)
            {
                _loggs.LogError(ex, "Something when wrong trying to update a new group");
                throw;
            }
        }
    }
}
