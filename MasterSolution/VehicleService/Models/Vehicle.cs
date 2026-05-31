using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleService.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression(@"^[VH]{2}-\d{4}$", ErrorMessage = "Registration ID must be in the format VH-1234 (VH- followed by 4 digits).")]

        public string RegistrationId { get; set; }

        [Required]
        public string Type { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int ManfacturYear { get; set; }
        [Required]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Chasis number must be exactly 17 characters long.")]
        public string ChasisNumber { get; set; }

        [Required]
        public DateTime RegistrationEndDate { get; set; }
        [Required]
        public DateTime PollutionCheckDate { get; set; }
        [Required]
        public DateTime InsuranceEndDate { get; set; }

    }
}
