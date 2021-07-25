using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.DTO;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
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
        /// <response code="500">Something went wrong</response>   
        [HttpPost]
        public async Task<Group> Post(GroupDto group)
        {
            return await _groupDomain.Save(new Group() { Name = group.Name, Amps = group.Amps });
        }

        /// <summary>
        /// Update a specific group.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /Groups/1
        ///     {
        ///        "name": "Group 1",
        ///        "amps": 1.2 //Should be greater than 0
        ///     }
        ///
        /// </remarks>
        /// <returns>The group updated</returns>
        /// <response code="200">The group updated</response>
        /// <response code="400">The group is not valid</response>    
        /// <response code="404">The group does not exist</response>
        /// <response code="500">Something went wrong</response>    
        [HttpPut("id")]
        public async Task<Group> Put(int id, GroupDto group)
        {
            return await _groupDomain.Update(new Group() { Id = id, Name = group.Name, Amps = group.Amps });
        }

        /// <summary>
        /// Delete a specific group.
        /// </summary>
        /// <remarks>
        /// <response code="200">The group has been deleted</response>  
        /// <response code="404">The group does not exist</response>
        /// <response code="500">Something went wrong</response>    
        [HttpDelete("id")]
        public async Task Delete(int id)
        {
            await _groupDomain.Delete(id);
        }
    }
}
