using InsuranceProject.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceProject.Models
{
    public class Premium:BaseEntity
    {
        public Guid PremiumId { get; set; }
        public string? Status { get; set; } = "UnPaid";
        public double? Amount { get; set; }

        public DateTime? DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }

        [ForeignKey("Agent")]
        public Guid? AgentId { get; set; }
        [ForeignKey("Policy")]
        public Guid PolicyId { get; set; }
    }
}
