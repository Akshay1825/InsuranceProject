using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IInsuranceScheme
    {
        public Guid Add(InsuranceSchemeDto insuranceSchemeDto);
        public InsuranceSchemeDto Get(Guid id);
        public List<InsuranceSchemeDto> GetAll();
        public bool Update(InsuranceSchemeDto insuranceSchemeDto);
        public bool Delete(Guid id);
        public PageList<InsuranceScheme> GetAll(FilterParameter filter, Guid planId);

        public List<InsuranceScheme> GetAllSchemes(Guid id);
    }
}
