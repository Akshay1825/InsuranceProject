using InsuranceProject.Types;

namespace InsuranceProject.Helper
{
    public class PolicyFilter:PageParameter
    {
        public Guid? Id { get; set; }
        public Status? Status { get; set; }

        public Guid? customerId { get; set; }

        public Guid? agentId { get; set; }
    }
}
