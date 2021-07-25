using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.DTO;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCharging.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupDomain _groupDomain;
        public GroupsController(IGroupDomain groupDomain)
        {
            _groupDomain = groupDomain;
        }

        /// <summary>
        /// Adds a specific group.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Groups
        ///     {
        ///        "name": "Group 1",
        ///        "amps": 1.2 //Should be greater than 0
        ///     }
        ///
        /// </remarks>
        /// <param name="group"></param>  
        /// <returns>The group created</returns>
        /// <response code="200">Return the group created</response>
        /// <response code="400">The group is not valid</response>    
        [HttpPost]
        public async Task<Group> Post(GroupDto group)
        {
            return await _groupDomain.Save(new Group() { Name = group.Name, Amps = group.Amps });
        }
    }
}
