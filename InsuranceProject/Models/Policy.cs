using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Models
{
    public class Policy
    {
        [Key]
        public Guid PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
    }
}
