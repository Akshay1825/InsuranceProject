using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Models
{
    public class Nominee:BaseEntity
    {
        [Key]
        public Guid NomineeId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only alphabetical characters are allowed.")]
        public string NomineeName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string NomineeRelation { get; set; }
        public Policy? Policy { get; set; }

        [ForeignKey("Policy")]
        public Guid? PolicyNumber { get; set; }
    }
}
