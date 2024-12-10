using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IAgentService
    {
        public Guid Add(AgentRegisterDto agentRegisterDto);
        public bool Update(AgentDto agentDto);
        public PagedResult<AgentDto> GetAll(FilterParameter filterParameter);
        public AgentDto Get(Guid id);
        public bool Delete(Guid id);
        public bool ChangePassword(ChangePasswordDto passwordDto);

        public Agent GetByUserName(string userName);
    }
}
