using InsuranceProject.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Xml.Linq;

namespace InsuranceProject.Models
{
    public class Policy:BaseEntity
    {
        [Key]
        public Guid PolicyId { get; set; }

        public int? PolicyNumber { get; set; }

        public string? SchemeName { get; set; }

        [ForeignKey("InsuranceScheme")]
        public Guid? InsuranceSchemeId { get; set; }
        public InsuranceScheme? InsuranceScheme { get; set; }

        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Required]
        public DateTime IssueDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Maturity Date is required.")]
        public DateTime MaturityDate { get; set; }

        [Required(ErrorMessage = "Premium Type is required.")]
        public Mode? PremiumType { get; set; }

        [Required(ErrorMessage = "Sum Assured is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Sum Assured must be greater than 0.")]
        public double SumAssured { get; set; }
        public int? InvestmentAmount { get; set; }

        public int? TotalPremiumNumber { get; set; }

        [Required(ErrorMessage = "Policy Term is required.")]
        public long PolicyTerm { get; set; }

        [Required(ErrorMessage = "Premium Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Premium Amount must be greater than 0.")]
        public double PremiumAmount { get; set; }

        public double? InstallmentAmount { get; set; }
        public double? TotalPaidAmount { get; set; }

        public Status? Status { get; set; } = Types.Status.Pending;

        [ForeignKey("Agent")]
        public Guid? AgentId { get; set; }
        public Agent? Agent { get; set; }

        public string? Nominee { get; set; }

        public string? NomineeRelation { get; set; }

        public List<Installment>? Installments { get; set; }
        public List<Nominee>? Nominees { get; set; }
        public int? Payments { get; set; } = 0;

        public List<Document>? Documents { get; set; }

        [ForeignKey("TaxSetting")]
        public Guid? TaxId { get; set; }
        public TaxSettings? TaxSettings { get; set; }

        [ForeignKey("Payment")]
        public Guid? PaymentId { get; set; }

        public DateTime? CancellationDate { get; set; }

        [ForeignKey("InsuranceSetting")]
        public Guid? InsuranceSettingId { get; set; }
        public InsuranceSettings? InsuranceSettings { get; set; }
        public Claimm? Claimm { get; set; }
    }
}
