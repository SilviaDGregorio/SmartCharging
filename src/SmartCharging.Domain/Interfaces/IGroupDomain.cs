using SmartCharging.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Interfaces
{
    public interface IGroupDomain
    {
        Task<Group> Save(Group group);
    }
}