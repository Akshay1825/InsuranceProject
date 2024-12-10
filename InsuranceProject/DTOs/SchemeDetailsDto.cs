using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class SchemeDetailsDto
    {
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
        public bool? Status { get; set; }
        //public int SchemeId { get; set; }
    }
}
