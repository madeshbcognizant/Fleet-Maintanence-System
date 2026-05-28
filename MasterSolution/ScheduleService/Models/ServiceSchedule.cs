namespace ScheduleService.Models
{
    public class ServiceSchedule
    {
        [Key]
        public string ServiceId { get; set; }
        public string RegId { get; set; }
        public string Type { get; set; }
        public int Model { get; set; }

        public string NameOfService { get; set; }
        public string Description { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int Kilometer { get; set; }
        public string Status { get; set; }

    }
}
