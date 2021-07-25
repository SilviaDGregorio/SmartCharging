using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Api.DTO
{
    public class ChargeStationDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
