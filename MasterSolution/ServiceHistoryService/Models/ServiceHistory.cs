using System.ComponentModel.DataAnnotations;

namespace ServiceHistoryService.Models
{
    public class ServiceHistory
    {
        [Key]
        public string HistoryId { get; set; }
        public string RegId { get; set; }
        public string ServiceId { get; set; }
        public string ServiceType { get; set; }
        public double TotalLabourCost { get; set; }
        public double TotalPartsCost { get; set; }
        public double TotalCost { get; set; }
        public DateTime CompletedDate { get; set; }= DateTime.Now;
    }
}
