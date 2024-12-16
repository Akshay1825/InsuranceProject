using InsuranceProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using InsuranceProject.Types;

namespace InsuranceProject.DTOs
{
    public class DocumentDto
    {
        [Key]
        public Guid Id { get; set; }
        public IFormFile? File { get; set; }
        public string? Name { get; set; }
        public string? FilePath { get; set; }
        public DocumentType? DocType { get; set; }
        public int? Status { get; set; } = 0;

        public string? Note { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
