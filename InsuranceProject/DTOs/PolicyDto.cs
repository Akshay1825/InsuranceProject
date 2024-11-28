using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class PolicyDto
    {
        [Key]
        public Guid PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
    }
}
