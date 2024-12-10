using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class ComplaintDto
    {
        public Guid ComplaintId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string ComplaintName { get; set; }
        public string? ComplaintMessage { get; set; }

        public string? Response {  get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime DateOfComplaint { get; set; }

        public Guid? CustomerId { get; set; }
        public bool? Status { get; set; }
    }
}
