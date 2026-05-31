namespace OdometerService.Models
{
    public class OdometerDTO
    {
        public string RegId { get; set; }
        public int Current_Kilometer { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
