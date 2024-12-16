

using InsuranceProject.Types;

namespace InsurancePolicy.DTOs
{
    public class CommissionResponseDto
    {
        public Guid CommissionId { get; set; }
        public string? CommissionType { get; set; } // Change from enum to string
        public DateOnly Date { get; set; }

        public int? Status { get; set; } = 0;
        public double Amount { get; set; }
        public Guid AgentId { get; set; }
        public string AgentName { get; set; } // Optional for display
        public Guid? PolicyId { get; set; } // Nullable for optional linkage
        public string PolicyName { get; set; } // Optional for display

        public int? PolicyNumber { get; set; }
    }
}
