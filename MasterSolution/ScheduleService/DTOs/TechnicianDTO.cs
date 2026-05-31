namespace ScheduleService.DTOs
{
    public class TechnicianDTO
    {
        public string TechnicianId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Skill { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
