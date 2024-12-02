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
        private readonly IRepository<Agent> _agentRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private Guid _roleId = new Guid("a8f1b121-fd38-4733-50be-08dd115dd6c7");

        public AgentService(IRepository<Agent> repository, IMapper mapper, IRepository<Role> roleRepository, IRepository<User> userRepository)
        {
            _agentRepository = repository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }
        public Guid Add(AgentRegisterDto agentRegisterDto)
        {
            var user = new User()
            {
                UserName = agentRegisterDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(agentRegisterDto.Password),
                Status = true,
                RoleId = _roleId
            };
            _userRepository.Add(user);

            var role = _roleRepository.Get(_roleId);
            role.Users.Add(user);

            agentRegisterDto.UserId = user.Id;

            var agent = _mapper.Map<Agent>(agentRegisterDto);
            _agentRepository.Add(agent);
            return agent.Id;
        }

        public bool ChangePassword(ChangePasswordDto passwordDto)
        {
            var agent = _agentRepository.GetAll().AsNoTracking().Include(a => a.User).Where(a => a.User.UserName == passwordDto.UserName).FirstOrDefault();
            if (agent != null)
            {
                if (BCrypt.Net.BCrypt.Verify(passwordDto.Password, agent.User.PasswordHash))
                {
                    agent.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordDto.NewPassword);
                    _agentRepository.Update(agent);
                    return true;
                }

            }
            return false;
        }

        public bool Delete(Guid id)
        {
            var agent = _agentRepository.Get(id);
            if (agent != null)
            {
                _agentRepository.Delete(agent);
                return true;
            }
            return false;
        }

        public AgentDto Get(Guid id)
        {
            var agent = _agentRepository.Get(id);
            if (agent != null)
            {
                var agentDto = _mapper.Map<AgentDto>(agent);
                return agentDto;
            }
            throw new Exception("No such agent exist");
        }

        public List<Agent> GetAll()
        {
            var agents = _agentRepository.GetAll().Include(a => a.User).ToList();
            //var agentDtos = _mapper.Map<List<AgentDto>>(agents);
            return agents;
        }

        public bool Update(AgentDto agentDto)
        {
            var existingAgent = _agentRepository.GetAll().AsNoTracking().Where(u => u.Id == agentDto.Id);
            if (existingAgent != null)
            {
                var agent = _mapper.Map<Agent>(agentDto);
                _agentRepository.Update(agent);
                return true;
            }
            return false;
        }
    }
}
