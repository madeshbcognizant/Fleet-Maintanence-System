using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdometerService.Models
{
    public class Odometer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ReadingId { get; set; }
        public string RegId { get; set; }
        public int Current_Kilometer { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
