namespace SmartCharging.Api.DTO
{
    public class GroupReturn : GroupDto
    {
        public int Id { get; set; }
        public float UsedAmps { get; set; }
    }
}
