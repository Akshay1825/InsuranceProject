using InsuranceProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class EmployeeDto
    {
        [Key]
        public Guid EmployeeId { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "First name should not greater than 15")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Last name should not greater than 15")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits.")]
        public long MobileNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public double Salary { get; set; }
        public User User { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Agent> Agents { get; set; }
    }
}
