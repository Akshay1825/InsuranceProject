using InsuranceProject.Types;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Models
{
    public class SchemeDetails:BaseEntity
    {
        [Key]
        public Guid DetailId { get; set; }
        public string SchemeImage { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int MinAmount { get; set; }
        [Required]
        public int MaxAmount { get; set; }
        [Required]
        public int MinAge { get; set; }
        [Required]
        public int MaxAge { get; set; }
        public ClaimType? ClaimType { get; set; }
        public bool? Status { get; set; }
        public InsuranceScheme? InsuranceScheme { get; set; }

    }
}
