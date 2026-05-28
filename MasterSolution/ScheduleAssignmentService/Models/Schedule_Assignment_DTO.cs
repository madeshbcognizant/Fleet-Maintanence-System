namespace ScheduleAssignmentService.Models
{
    public class Schedule_Assignment_DTO
    {
        public string ServiceId { get; set; }
        public string TechnicianId { get; set; }
        public string RegId { get; set; }
        public string NameOfService { get; set; }
        public string Description { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int Km { get; set; }
        public string Status { get; set; }
    }
}
