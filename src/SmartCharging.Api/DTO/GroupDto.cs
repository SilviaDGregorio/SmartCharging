using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Api.DTO
{
    public class GroupDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [RequiredGreaterThanZeroValidation()]
        public float Amps { get; set; }
    }
}
