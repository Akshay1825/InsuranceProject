using InsuranceProject.Models;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class RoleDto
    {

        [Key]
        public Guid RoleId { get; set; }
        [Required]
        public Role RoleName { get; set; }
        public bool Status { get; set; }
    }
}
