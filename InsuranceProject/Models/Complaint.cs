using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceProject.Models
{
    public class Complaint:BaseEntity
    {
        [Key]
        public Guid ComplaintId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string ComplaintName { get; set; }
        public string? ComplaintMessage { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime DateOfComplaint { get; set; }
        public bool? Status { get; set; }

        public string? Response {  get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Customer")]

        public Guid? CustomerId { get; set; }
    }
}
