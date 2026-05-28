using System.ComponentModel.DataAnnotations; 

namespace MasterService.Models
{
    public class Master
    {
        [Key]
        public string? Type { get; set; }
        public string? NameOfService { get; set; }
        public int FreqDays { get; set; }
        public int FreqKm { get; set; }
        public string? Description { get; set; }

        public int PollutionCertificateRenewal { get; set; }

        public int InsuranceRenewal { get; set; }
        public int FCRenewal { get; set; }
        public int PermitRenewal { get; set; }
    }
}
