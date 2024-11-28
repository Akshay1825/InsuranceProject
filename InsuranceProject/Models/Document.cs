using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Models
{
    public class Document
    {
        [Key]
        public Guid DocumentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
    }
}
