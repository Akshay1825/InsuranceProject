using InsuranceProject.DTOs;
using InsuranceProject.Helper;

namespace InsuranceProject.Services
{
    public interface IInsurancePlanService
    {
        public Guid Add(InsurancePlanDto insurancePlanDto);
        public InsurancePlanDto Get(Guid id);
        public PagedResult<InsurancePlanDto> GetAll(FilterParameter filterParameter);
        public bool Update(InsurancePlanDto insurancePlanDto);
        public bool Delete(Guid id);
    }
}
