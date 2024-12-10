using InsuranceProject.Models;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class CustomerRegistrationDto
    {
        public Guid CustomerId { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "First name should not greater than 15")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "First name should not greater than 15")]
        public string LastName { get; set; }


        public DateOnly? BirthDate { get; set; }

        public string Address {  get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits.")]
        public long MobileNumber { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public Guid? UserId { get; set; }
        public Guid? AgentId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }

    }
}
