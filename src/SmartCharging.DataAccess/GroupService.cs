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

        public async Task Delete(int id)
        {
            var group = await GetById(id);
            _dbContext.Groups.Remove(group);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Group> Save(Group group)
        {
            await _dbContext.Groups.AddAsync(group);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Groups.FindAsync(group.Id);
        }

        public async Task<Group> Update(Group group)
        {
            var groupDb = await GetById(group.Id);
            groupDb.Amps = group.Amps;
            groupDb.UsedAmps = group.UsedAmps;
            groupDb.Name = group.Name;
            await _dbContext.SaveChangesAsync();
            return groupDb;
        }

        public async Task<Group> GetById(int id)
        {
            var groupDb = await _dbContext.Groups.FindAsync(id);
            if (groupDb == null)
            {
                throw new KeyNotFoundException($"The group with id: {id} does not exist");
            }
            return groupDb;
        }
    }
}
