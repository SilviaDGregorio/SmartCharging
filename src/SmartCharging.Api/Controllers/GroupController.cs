using Microsoft.AspNetCore.Mvc;
using SmartCharging.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCharging.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupDomain _groupDomain;
        public GroupController(IGroupDomain groupDomain)
        {
            _groupDomain = groupDomain;
        }

        [HttpPost]
        public async Task<IEnumerable<Group>> Post(Group group)
        {
            return await _groupDomain.
            return new List<Group>();
        }
    }
}
