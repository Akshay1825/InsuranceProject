using InsuranceProject.Types;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace InsuranceProject.Models
{
    public class Role:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Roles RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}
