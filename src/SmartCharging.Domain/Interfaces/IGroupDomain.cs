using SmartCharging.Domain.Entities;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Interfaces
{
    public interface IGroupDomain
    {
        Task<Group> Save(Group group);
        Task<Group> Update(Group group);
        Task Delete(int id);
    }
}