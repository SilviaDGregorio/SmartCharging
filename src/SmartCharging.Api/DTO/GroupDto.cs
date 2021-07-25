using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Api.DTO
{
    public class GroupDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [RequiredGreaterThanZeroValidation()]
        public float Amps { get; set; }
    }
}
