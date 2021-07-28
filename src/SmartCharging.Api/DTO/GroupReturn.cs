using System;

namespace SmartCharging.Api.DTO
{
    public class GroupReturn : GroupDto
    {
        public Guid Id { get; set; }
        public float UsedAmps { get; set; }
    }
}
