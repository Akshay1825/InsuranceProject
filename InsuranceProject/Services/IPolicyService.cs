using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IPolicyService
    {
        public Guid Add(PolicyDto policy);
        public PolicyDto Get(Guid id);
        public List<PolicyDto> GetAll();
        public bool Update(PolicyDto policy);
        public bool Delete(Guid id);
        public PagedResult<PolicyDto> GetPoliciesWithCustomerId(FilterParameter filterParameter, Guid userID);

        public PagedResult<PolicyDto> GetAll(FilterParameter filterParameter);

        public Customer GetUser(PolicyDto policyDto);

        public bool UpdatePolicy(PolicyDto policydto);

        public PagedResult<PolicyDto> GetAlll(FilterParameter filterParameter, Guid Id);
    }
}
