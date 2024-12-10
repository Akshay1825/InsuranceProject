using InsuranceProject.DTOs;
using InsuranceProject.Helper;

namespace InsuranceProject.Services
{
    public interface IClaimService
    {
        public Guid Add(ClaimDto claimDto);
        public ClaimDto Get(Guid id);
        public PagedResult<ClaimDto> GetAll(FilterParameter filterParameter);
        public bool Update(ClaimDto claimDto);
        public bool Delete(Guid id);
    }
}
