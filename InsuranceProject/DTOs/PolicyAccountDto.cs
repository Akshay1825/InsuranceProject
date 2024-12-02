namespace InsuranceProject.DTOs
{
    public class PolicyAccountDto
    {
        public string BankName { get; set; }
        public string IFSC { get; set; }
        public long AccountNumber { get; set; }

        public Guid CustomerId { get; set; }
    }
}
