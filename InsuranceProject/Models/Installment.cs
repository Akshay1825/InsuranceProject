using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using InsuranceProject.Types;

namespace InsuranceProject.Models
{
    public class Installment:BaseEntity
    {
        [Key]
        public Guid InstallmentId { get; set; }

        [ForeignKey("Policy")]
        public Guid? PolicyId { get; set; } // Made optional
        public Policy InsurancePolicy { get; set; } // Navigation property

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required]
        public double AmountDue { get; set; }

        public double? AmountPaid { get; set; }

        public Status? Status { get; set; } = Types.Status.Pending;

        public string? PaymentReference { get; set; }
    }
}
