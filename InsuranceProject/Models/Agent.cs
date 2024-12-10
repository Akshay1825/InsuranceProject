using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Models
{
    public class Agent:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name must be in 2 to 20 characters.")]
        public string FirstName { get; set; }
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name must be in 2 to 20 characters.")]
        public string LastName { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public long MobileNumber { get; set; }

        public string? UserName { get; set; }

        public bool? Status { get; set; } = true;
        public double? CommisionEarned { get; set; }
        public User? User { get; set; }
        [ForeignKey("User")]
        public Guid? UserId { get; set; }

        public int? CustomerCount { get; set; }
        public List<Customer>? Customers { get; set; }
        public double? TotalCommissionEarned { get; set; }
        public double? TotalWithdrawalAmount { get; set; }
    }
}
