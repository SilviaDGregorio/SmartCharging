using AutoMapper;
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
        private readonly IMapper _mapper;

        public GroupsController(IGroupDomain groupDomain, IMapper mapper)
        {
            _groupDomain = groupDomain;
            _mapper = mapper;
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
        public async Task<GroupReturn> Post(GroupDto group)
        {
            return _mapper.Map<GroupReturn>(await _groupDomain.Save(_mapper.Map<Group>(group)));
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
        public async Task<GroupReturn> Put(int id, GroupDto group)
        {
            var groupEntity = _mapper.Map<Group>(group);
            groupEntity.Id = id;
            return _mapper.Map<GroupReturn>(await _groupDomain.Update(groupEntity));
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
