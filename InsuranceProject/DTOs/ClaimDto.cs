using InsuranceProject.Types;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class ClaimDto
    {
        public Guid ClaimId { get; set; }
        public double? ClaimAmount { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime ClaimDate { get; set; }
        //[Required(ErrorMessage = "This field is required")]
        //public string BankAccountNo { get; set; }
        //[Required(ErrorMessage = "This field is required")]
        //public string? BankIFSCCode { get; set; }
        public ClaimStatus? Status { get; set; }=Types.ClaimStatus.Pending;

        public Guid? PolicyId { get; set; }
        public int? PoliciesCount { get; set; }
    }
}
