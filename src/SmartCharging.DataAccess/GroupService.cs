using Microsoft.EntityFrameworkCore;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
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

    }
}
