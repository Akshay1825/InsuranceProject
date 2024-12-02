using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Models
{
    public class Admin
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "First name should not greater than 15")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Last name should not greater than 15")]
        public string LastName { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public List<Employee>? Employees { get; set; }
        public List<Customer>? Customers { get; set; }
        public List<Agent>? Agents { get; set; }
        public List<Policy>? Policies { get; set; }
    }
}
