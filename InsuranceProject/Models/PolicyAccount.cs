using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Models
{
    public class PolicyAccount
    {
        [Key]
        public Guid Id { get; set; }
        public string BankName { get; set; }
        public string IFSC { get; set; }
        public long AccountNumber { get; set; }

        public Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
    }
}
