using AutoMapper;
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
        private readonly IMapper _mapper;

        public ConnectorsController(IConnectorDomain connectorDomain, IMapper mapper)
        {
            _connectorDomain = connectorDomain;
            _mapper = mapper;
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
        public async Task<ConnectorReturn> Post(int chargeStationId, ConnectorDto connector)
        {
            var connectorEntity = _mapper.Map<Connector>(connector);
            connectorEntity.ChargeStationId = chargeStationId;
            return _mapper.Map<ConnectorReturn>(await _connectorDomain.Save(connectorEntity));
        }
    }
}
