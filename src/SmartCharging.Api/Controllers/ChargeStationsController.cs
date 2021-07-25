using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.DTO;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
using System.Threading.Tasks;

namespace SmartCharging.Api.Controllers
{
    [ApiController]
    [Route("Groups/{groupId}/[controller]")]
    public class ChargeStationsController : ControllerBase
    {
        private readonly IChargeStationDomain _stationDomain;
        public ChargeStationsController(IChargeStationDomain stationDomain)
        {
            _stationDomain = stationDomain;
        }

        /// <summary>
        /// Adds a specific charge station.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Groups/1/chargeStations
        ///     {
        ///        "name": "Station 1"        
        ///     }
        ///
        /// </remarks>
        /// <param name="groupId">Group id</param>
        /// <param name="ChargeStationDto">The chargeStation to be added</param>  
        /// <returns>The charge station created</returns>
        /// <response code="200">Return the charge station created</response>
        /// <response code="400">The charge station  is not valid</response>   
        /// <response code="500">Something went wrong</response>   
        [HttpPost]
        public async Task<ChargeStation> Post(int groupId, ChargeStationDto chargeStation)
        {
            return await _stationDomain.Save(new ChargeStation() { Name = chargeStation.Name, GroupId = groupId });
        }
    }
}
