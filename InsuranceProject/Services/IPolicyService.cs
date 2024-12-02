using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface IPolicyService
    {
        public Guid Add(PolicyDto policy);
        public PolicyDto Get(Guid id);
        public List<PolicyDto> GetAll();
        public bool Update(PolicyDto policy);
        public bool Delete(Guid id);
    }
}
