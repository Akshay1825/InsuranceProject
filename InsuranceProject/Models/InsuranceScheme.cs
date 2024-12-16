using InsuranceProject.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceProject.Models
{
    public class InsuranceScheme:BaseEntity
    {
        [Key]
        public Guid SchemeId { get; set; }

        [Required(ErrorMessage = "Scheme Name is required.")]
        [StringLength(100, ErrorMessage = "Scheme Name should not exceed 100 characters.")]
        public string SchemeName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Minimum amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Minimum amount must be a positive number.")]
        public double MinAmount { get; set; }

        [Required(ErrorMessage = "Maximum amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Maximum amount must be a positive number.")]
        public double MaxAmount { get; set; }

        [Required(ErrorMessage = "Minimum investment time is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum investment time must be at least 1 month.")]
        public int MinInvestTime { get; set; } // Investment time in months

        [Required(ErrorMessage = "Maximum investment time is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Maximum investment time must be at least 1 month.")]
        public int MaxInvestTime { get; set; }

        [Required(ErrorMessage = "Minimum age is required.")]
        [Range(18, 100, ErrorMessage = "Minimum age must be between 18 and 100.")]
        public int MinAge { get; set; }

        [Required(ErrorMessage = "Maximum age is required.")]
        [Range(18, 100, ErrorMessage = "Maximum age must be between 18 and 100.")]
        public int MaxAge { get; set; }

        [Required(ErrorMessage = "Profit ratio is required.")]
        [Range(0.0, 100.0, ErrorMessage = "Profit ratio must be between 0 and 100%.")]
        public double ProfitRatio { get; set; }

        [Required(ErrorMessage = "Registration commission ratio is required.")]
        [Range(0.0, 100.0, ErrorMessage = "Registration commission ratio must be between 0 and 100%.")]
        public double RegistrationCommRatio { get; set; }

        [Required(ErrorMessage = "Installment commission ratio is required.")]
        [Range(0.0, 100.0, ErrorMessage = "Installment commission ratio must be between 0 and 100%.")]
        public double InstallmentCommRatio { get; set; }

        public bool? Status { get; set; } = true;
        [ForeignKey("InsurancePlan")]
        public Guid? PlanId { get; set; }
        public InsurancePlan? InsurancePlan { get; set; }

        public List<Policy>? Policies { get; set; }
        public List<DocumentType>? RequiredDocuments { get; set; } = new List<DocumentType>();

        public bool? IsActive { get; set; } = true;
    }
}
