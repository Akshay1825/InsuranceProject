using InsuranceProject.Models;
using InsuranceProject.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceProject.DTOs
{
    public class PolicyDto
    {
        [Key]
        public Guid PolicyId { get; set; }

        public int? PolicyNumber { get; set; }

        public string? SchemeName { get; set; }

        [ForeignKey("InsuranceScheme")]
        public Guid? InsuranceSchemeId { get; set; }
        //public InsuranceScheme InsuranceScheme { get; set; }

        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }
        //public Customer Customer { get; set; }

        [Required]
        public DateTime IssueDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Maturity Date is required.")]
        public DateTime? MaturityDate { get; set; }

        [Required(ErrorMessage = "Premium Type is required.")]
        public Mode? PremiumType { get; set; }

        [Required(ErrorMessage = "Sum Assured is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Sum Assured must be greater than 0.")]
        public double SumAssured { get; set; }

        [Required(ErrorMessage = "Policy Term is required.")]
        public long PolicyTerm { get; set; }

        [Required(ErrorMessage = "Premium Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Premium Amount must be greater than 0.")]
        public double PremiumAmount { get; set; }

        public int? InvestmentAmount{ get; set; }

        public int? TotalPremiumNumber { get; set; }

        public string? Nominee { get; set; }

        public string? NomineeRelation { get; set; }

        public double? InstallmentAmount { get; set; }
        public double? TotalPaidAmount { get; set; }

        public Status? Status { get; set; } = Types.Status.Pending;

        [ForeignKey("Agent")]
        public Guid? AgentId { get; set; }
        
        [ForeignKey("TaxSetting")]
        public Guid? TaxId { get; set; }

        public DateTime? CancellationDate { get; set; }

        public int? Payments { get; set; } = 0;

        [ForeignKey("InsuranceSetting")]
        public Guid? InsuranceSettingId { get; set; }

        public Guid? PaymentId { get; set; }
    }
}
