using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface IPolicyAccountService
    {
        public Guid Add(PolicyAccountDto policyAccountDto);
        public void Delete(Guid id);
        public void Update(PolicyAccountDto policyAccountDto);
    }
}
