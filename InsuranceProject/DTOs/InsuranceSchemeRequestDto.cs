using InsuranceProject.Types;
using System.ComponentModel.DataAnnotations;

namespace InsurancePolicy.DTOs
{
    public class InsuranceSchemeRequestDto
        {
            public Guid? SchemeId { get; set; }

            [Required(ErrorMessage = "Scheme Name is required.")]
            [StringLength(100, ErrorMessage = "Scheme Name should not exceed 100 characters.")]
            public string SchemeName { get; set; }

            [Required(ErrorMessage = "Scheme image is required.")]
            public string SchemeImage { get; set; }

            [Required(ErrorMessage = "Description is required.")]
            [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
            public string Description { get; set; }

            [Required(ErrorMessage = "Minimum amount is required.")]
            public double MinAmount { get; set; }

            [Required(ErrorMessage = "Maximum amount is required.")]
            public double MaxAmount { get; set; }

            [Required(ErrorMessage = "Minimum investment time is required.")]
            public int MinInvestTime { get; set; }

            [Required(ErrorMessage = "Maximum investment time is required.")]
            public int MaxInvestTime { get; set; }

            [Required(ErrorMessage = "Minimum age is required.")]
            public int MinAge { get; set; }

            [Required(ErrorMessage = "Maximum age is required.")]
            public int MaxAge { get; set; }

            [Required(ErrorMessage = "Profit ratio is required.")]
            [Range(0.0, 100.0, ErrorMessage = "Profit ratio must be between 0 and 100%.")]
            public double ProfitRatio { get; set; }

            [Required(ErrorMessage = "Registration commission ratio is required.")]
            public double RegistrationCommRatio { get; set; }

            [Required(ErrorMessage = "Installment commission ratio is required.")]
            public double InstallmentCommRatio { get; set; }

            public bool Status { get; set; }
            public Guid PlanId { get; set; }
            [Required]
            public List<DocumentType> RequiredDocuments { get; set; }
        }
    }
