using InsuranceProject.Types;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Models
{
    public class Claimm:BaseEntity
    {
        [Key]

        public Guid ClaimId { get; set; }

        public double? ClaimAmount { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime? ClaimDate { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankIFSCCode { get; set; }
        public int? Status { get; set; } = 0;

        public Guid? PolicyId { get; set; }

        public List<Policy>? Policies { get; set; }
    }
}
