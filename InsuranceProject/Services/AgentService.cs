using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository<Agent> _repository;
        private readonly IMapper _mapper;

        public AgentService(IRepository<Agent> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Guid Add(AgentDto agentDto)
        {
            var agent = _mapper.Map<Agent>(agentDto);
            _repository.Add(agent);
            return agent.AgentId;
        }

        public bool Delete(AgentDto agentDto)
        {
            var agent = _mapper.Map<Agent>(agentDto);
            var existingAgent = _repository.GetById(agent.AgentId);
            if (existingAgent != null)
            {
                _repository.Delete(existingAgent);
                return true;
            }
            return false;
        }

        public AgentDto Get(Guid id)
        {
            var agent = _repository.GetById(id);
            if (agent == null)
            {
                throw new AgentNotFoundException("Agent Not Found");
            }
            var agentDto = _mapper.Map<AgentDto>(agent);
            return agentDto;
        }

        public List<AgentDto> GetAll()
        {
            var agents = _repository.GetAll().ToList();
            List<AgentDto> result = _mapper.Map<List<AgentDto>>(agents);
            return result;
        }

        public AgentDto Update(AgentDto agentDto)
        {
            var existingAgent = _mapper.Map<Agent>(agentDto);
            var updatedAgent = _repository.GetAll().AsNoTracking().FirstOrDefault(x => x.AgentId == existingAgent.AgentId);
            if (updatedAgent != null)
            {
                _repository.Update(updatedAgent);
            }
            var updatedAgentDto = _mapper.Map<AgentDto>(updatedAgent);
            return updatedAgentDto;
        }
    }
}
