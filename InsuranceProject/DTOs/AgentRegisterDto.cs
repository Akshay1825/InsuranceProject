using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class AgentRegisterDto
    {
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

        public string? BankAccountNumber { get; set; }

        public string? IfscCode { get; set; }
        [Required]
        public long MobileNumber { get; set; }

        public bool? Status { get; set; } = false;
        public Guid? UserId { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "username must be in 5 to 20 characters")]
        public string UserName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "password must be in 7 to 20 characters")]
        public string Password { get; set; }
        //[Required]
        //[StringLength(20, MinimumLength = 7, ErrorMessage = "password must be in 7 to 20 characters")]
        //public string ConfirmPassword { get; set; }
    }
}
