using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using InsuranceProject.Types;

namespace InsuranceProject.Models
{
    public class Document:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? FilePath { get; set; }
        public DocumentType? DocType { get; set; }

        public int? Status { get; set; } = 0;

        public string? Note { get; set; }

        //public Customer Customer { get; set; }
        //[ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }
    }
}
