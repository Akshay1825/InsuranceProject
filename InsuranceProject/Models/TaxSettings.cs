using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Models
{
    public class TaxSettings:BaseEntity
    {
        [Key]
        public Guid TaxId { get; set; } // Changed to Guid for consistency

        [Required(ErrorMessage = "Tax Percentage is required.")]
        [Range(0, 100, ErrorMessage = "Tax Percentage must be between 0 and 100.")]
        public double? TaxPercentage { get; set; }

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
