using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface IAgentService
    {
        public List<AgentDto> GetAll();
        public AgentDto Get(Guid id);
        public Guid Add(AgentDto agentDto);
        public AgentDto Update(AgentDto agentDto);
        public bool Delete(AgentDto agentDto);
    }
}
