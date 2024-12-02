using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class AdminRegisterDto
    {
        [Required]
        [StringLength(15, ErrorMessage = "First name should not greater than 15")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Last name should not greater than 15")]
        public string LastName { get; set; }
       
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "username must be in 5 to 20 characters")]
        public string UserName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "password must be in 7 to 20 characters")]
        public string Password { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "password must be in 7 to 20 characters")]
        public string ConfirmPassword { get; set; }
        public Guid? UserId { get; set; }
    }
}
