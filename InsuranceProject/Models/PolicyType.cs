using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Models
{
    public class PolicyType:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Policy> Policies { get; set; }
    }
}
