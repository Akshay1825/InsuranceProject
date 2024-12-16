using InsuranceProject.Helper;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface ICommissionService
    {
        public PageList<Commission> GetAll(Guid AgentId, DateFilter dateFilter);

        public bool UpdateCustomer(Commission commission);

        public PageList<Commission> GetAll(DateFilter dateFilter);
    }
}
