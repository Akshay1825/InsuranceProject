using InsuranceProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class CustomerDto
    {
        [Key]
        public Guid CustomerId { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "First name should not greater than 15")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "First name should not greater than 15")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits.")]
        public long MobileNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Nominee { get; set; }
        public string NomineeRelation { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public Agent? Agent { get; set; }
        public List<Document> Documents { get; set; }

        public List<Policy> Policies { get; set; }
    }
}
