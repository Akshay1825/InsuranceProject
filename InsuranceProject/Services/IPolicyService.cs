using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface IPolicyService
    {
        public List<PolicyDto> GetAll();
        public PolicyDto Get(Guid id);
        public Guid Add(PolicyDto policyDto);
        public PolicyDto Update(PolicyDto policyDto);
        public bool Delete(PolicyDto policyDto);
    }
}
