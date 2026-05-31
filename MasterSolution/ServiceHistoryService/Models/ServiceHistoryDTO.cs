namespace ServiceHistoryService.Models
{
    public class ServiceHistoryDTO
    {
        public string RegId { get; set; }
        public string ServiceId { get; set; }
        public string ServiceType { get; set; }
        public double TotalLabourCost { get; set; }
        public double TotalPartsCost { get; set; }
       
    }
}
