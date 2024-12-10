using InsuranceProject.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceProject.Models
{
    public class Payment:BaseEntity
    {
        [Key]
        public Guid PaymentId { get; set; }
        //[Required]
        //public PaymentType? PaymentType { get; set; }

        public string? Status { get; set; } = "UnPaid";
        public double? Amount { get; set; }

        public int? indexId { get; set; }
        public DateTime? PaymentDate { get; set; }

        //[ForeignKey("Premium")]
        //public Guid? PremiumId { get; set; }
        public double? Tax { get; set; }
        public double? TotalPayment { get; set; }
        public string? CardHolderName { get; set; }
        public string? CardNumber { get; set; }
        public string? CVVNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        [ForeignKey("Policy")]
        public Guid PolicyId { get; set; }
        

        public List<Policy>? Policies { get; set; }
    }
}
