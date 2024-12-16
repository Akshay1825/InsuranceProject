using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using InsuranceProject.Types;

namespace InsuranceProject.Models
{
    public class Commission:BaseEntity
    {
        [Key]
        public Guid CommissionId { get; set; }

        public string? CommissionType { get; set; }

        public DateTime Date { get; set; }
        public int? Status { get; set; } = 0;

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Agent is required")]
        [ForeignKey("Agent")]
        public Guid? AgentId { get; set; }
        public Agent? Agent { get; set; }

        public int? policyNumber { get; set; }

        [ForeignKey("PolicyAccount")]
        public Guid? PolicyId { get; set; } // Nullable for optional linkage to a policy
        public Policy? PolicyAccount { get; set; }
    }
}
