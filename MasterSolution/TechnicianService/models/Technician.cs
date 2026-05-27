using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicianService.models
{
    public class Technician
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TechnicianId { get; set; }   
        public string TechnicianName { get; set; }
        public string TechnicianSkill { get; set; }

    }
}
