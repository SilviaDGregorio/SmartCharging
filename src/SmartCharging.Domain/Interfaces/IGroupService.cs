using SmartCharging.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Interfaces
{
    public interface IGroupService
    {
        Task<Group> Save(Group group);
        Task<Group> Update(Group group);
        Task Delete(Guid id);
        Task<Group> GetById(Guid id);
    }
}