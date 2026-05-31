using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleAssignmentService.Models
{
    public class Schedule_Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AssignmentId { get; set; }
        public string ServiceId { get; set; }
        public string TechnicianId { get; set; }
        public string RegId { get; set; }
        public string NameOfService { get; set; }
        public string Description { get; set; }
        public DateTime ScheduleDate { get; set; } // the date has to be updated by the fleet manager
        public int Km { get; set; }
        public string Status { get; set; }
        
    }
}
