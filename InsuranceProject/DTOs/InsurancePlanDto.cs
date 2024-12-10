using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class InsurancePlanDto
    {
        public Guid PlanId { get; set; }
        public string PlanName { get; set; }
        public bool? Status { get; set; }
        //public List<string>? SchemeNames { get; set; }
    }
}
