using System.ComponentModel.DataAnnotations;
using System.Data;

namespace InsuranceProject.Models
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }

        [Required]
        public Role RoleName { get; set; }
        public bool Status { get; set; }
    }
}
