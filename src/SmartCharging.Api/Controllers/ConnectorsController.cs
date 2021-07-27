using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.DTO;
using SmartCharging.Domain.Entities;
using SmartCharging.Domain.Interfaces;
using System.Threading.Tasks;

namespace SmartCharging.Api.Controllers
{
    [ApiController]
    [Route("ChargeStation/{chargeStationId}/[controller]")]
    public class ConnectorsController : ControllerBase
    {
        private readonly IConnectorDomain _connectorDomain;
        public ConnectorsController(IConnectorDomain connectorDomain)
        {
            _connectorDomain = connectorDomain;
        }

        /// <summary>
        /// Adds a specific connector.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /ChargeStation/1/connectors
        ///     {
        ///        "name": "Connector 1"        
        ///     }
        ///
        /// </remarks>
        /// <param name="chargeStationId">Charge station id</param>
        /// <param name="connector">The connector to be added</param>  
        /// <returns>The connector created</returns>
        /// <response code="200">Return the connector created</response>
        /// <response code="400">The connector  is not valid</response>   
        /// <response code="500">Something went wrong</response>   
        [HttpPost]
        public async Task<Connector> Post(int chargeStationId, ConnectorDto connector)
        {
            return await _connectorDomain.Save(new Connector() { ChargeStationId = chargeStationId, Amps = connector.Amps });
        }
    }
}
