using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCharging.DataAccess
{
    public class GroupService : IGroupService
    {
        private readonly SmartCharingDbContext _dbContext;

        public GroupService(SmartCharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Group> Save(Group group)
        {
            await _dbContext.Groups.AddAsync(group);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Groups.FindAsync(group.Id);
        }

        public async Task<Group> Update(Group group)
        {
            var groupDb = await _dbContext.Groups.FindAsync(group.Id);
            if (groupDb == null)
            {
                throw new KeyNotFoundException($"The group with id: {group.Id} does not exist");
            }
            _dbContext.Groups.Update(group);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Groups.FindAsync(group.Id);
        }
    }
}
