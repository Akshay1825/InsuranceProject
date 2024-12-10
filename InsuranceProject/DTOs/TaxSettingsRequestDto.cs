using System.ComponentModel.DataAnnotations;

namespace InsurancePolicy.DTOs
{
    public class TaxSettingsRequestDto
    {
        public Guid? TaxId { get; set; }
        [Required(ErrorMessage = "Tax Percentage is required.")]
        [Range(0, 100, ErrorMessage = "Tax Percentage must be between 0 and 100.")]
        public double? TaxPercentage { get; set; }
    }
}
