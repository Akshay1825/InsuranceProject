using InsuranceProject.Types;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        //public PaymentType? PaymentType { get; set; }
        public double Amount { get; set; }
        //public Guid? PremiumId { get; set; }
        public DateTime PaymentDate { get; set; }

        public int? indexId { get; set; }
        public double? Tax { get; set; }
        public double? TotalPayment { get; set; }
        
        public Guid? InsuranceSchemeId { get; set; }

        public int? PolicyNumber { get; set; }

        public Guid? AgentId { get; set; }
        public Guid PolicyId { get; set; }
        public string? Status { get; set; } = "Unpaid";
        public int? PoliciesCount { get; set; }
    }
}
