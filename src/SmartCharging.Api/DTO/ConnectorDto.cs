namespace SmartCharging.Api.DTO
{
    public class ConnectorDto
    {
        [RequiredGreaterThanZeroValidation()]
        public float Amps { get; set; }
    }
}
