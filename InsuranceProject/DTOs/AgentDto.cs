using InsuranceProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class AgentDto
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

        public string? userName { get; set; }

        public bool? Status { get; set; } = true;
        public double? CommisionEarned { get; set; }
        public Guid? UserId { get; set; }

        public int? CustomerCount { get; set; }
        public int? TotalCustomers { get; set; }
        public double? TotalCommissionEarned { get; set; }
        public double? TotalWithdrawalAmount { get; set; }
    }
}
