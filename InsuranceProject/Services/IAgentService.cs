using InsuranceProject.DTOs;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IAgentService
    {
        public Guid Add(AgentRegisterDto agentRegisterDto);
        public bool Update(AgentDto agentDto);
        public List<Agent> GetAll();
        public AgentDto Get(Guid id);
        public bool Delete(Guid id);
        public bool ChangePassword(ChangePasswordDto passwordDto);
    }
}
