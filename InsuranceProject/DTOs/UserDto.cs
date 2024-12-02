using InsuranceProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "username must be in 5 to 20 characters")]
        public string UserName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "password must be in 7 to 20 characters")]
        public string Password { get; set; }
        public Guid RoleId { get; set; }
    }
}
