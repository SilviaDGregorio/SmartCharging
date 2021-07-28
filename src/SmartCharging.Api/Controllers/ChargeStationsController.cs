using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.DTO;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace SmartCharging.Api.Controllers
{
    [ApiController]
    [Route("Groups/{groupId}/[controller]")]
    public class ChargeStationsController : ControllerBase
    {
        private readonly IChargeStationDomain _stationDomain;
        private readonly IMapper _mapper;

        public ChargeStationsController(IChargeStationDomain stationDomain, IMapper mapper)
        {
            _stationDomain = stationDomain;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a specific charge station.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Groups/4CBBF94A-C443-4C99-C666-08D951EDB213/chargeStations
        ///     {
        ///        "name": "Station 1"        
        ///     }
        ///
        /// </remarks>
        /// <param name="groupId">Group id</param>
        /// <param name="chargeStation">The chargeStation to be added</param>  
        /// <returns>The charge station created</returns>
        /// <response code="200">Return the charge station created</response>
        /// <response code="400">The charge station  is not valid</response>   
        /// <response code="500">Something went wrong</response>   
        [HttpPost]
        public async Task<ChargeStationReturn> Post(Guid groupId, ChargeStationDto chargeStation)
        {
            var chargeStationEntity = _mapper.Map<ChargeStation>(chargeStation);
            chargeStationEntity.GroupId = groupId;
            return _mapper.Map<ChargeStationReturn>(await _stationDomain.Save(chargeStationEntity));
        }

        /// <summary>
        /// Update a specific charge station.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /Groups/4CBBF94A-C443-4C99-C666-08D951EDB213/chargeStations/4CBBF94A-C443-4C99-C666-08D951EDB217
        ///     {
        ///        "name": "Station 1"
        ///     }
        ///
        /// </remarks>
        /// <returns>The charge station updated</returns>
        /// <response code="200">The chargeStation updated</response>
        /// <response code="400">The chargeStation is not valid</response>    
        /// <response code="404">The chargeStation does not exist</response>
        /// <response code="500">Something went wrong</response>  
        [HttpPut("{id}")]
        public async Task<ChargeStationReturn> Put(Guid groupId, Guid id, ChargeStationDto chargeStation)
        {
            var chargeStationEntity = _mapper.Map<ChargeStation>(chargeStation);
            chargeStationEntity.GroupId = groupId;
            chargeStationEntity.Id = id;
            return _mapper.Map<ChargeStationReturn>(await _stationDomain.Update(chargeStationEntity));
        }

        /// <summary>
        /// Delete a specific charge station.
        /// </summary>
        /// <remarks>
        /// <response code="200">The charge station has been deleted</response>  
        /// <response code="404">The charge station does not exist</response>
        /// <response code="500">Something went wrong</response>    
        [HttpDelete("{id}")]
        public async Task Delete(Guid groupId, Guid id)
        {
            await _stationDomain.Delete(groupId, id);
        }
    }
}
