using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
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
        private readonly IRepository<Customer> _customerRepository;
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
                UserName = agentRegisterDto.UserName,
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

        public PagedResult<AgentDto> GetAll(FilterParameter filterParameter)
        {
            var query = _agentRepository.GetAll().AsNoTracking();
            int totalCount = query.Count();
            var pagedCustomers = query
            .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
            .Take(filterParameter.PageSize)
                .ToList();

            var customerDtos = _mapper.Map<List<AgentDto>>(pagedCustomers);
            var pagedResult = new PagedResult<AgentDto>
            {
                Items = customerDtos,
                TotalCount = totalCount,
                PageSize = filterParameter.PageSize,
                CurrentPage = filterParameter.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasNext = filterParameter.PageNumber < (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasPrevious = filterParameter.PageNumber > 1
            };

            return pagedResult;
        }

        public Agent GetByUserName(string userName)
        {
            var customer = _agentRepository.GetAll().AsNoTracking().FirstOrDefault(u => u.UserName == userName);
            return customer;
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

        //public PageList<Agent> GetAll(FilterParameter filter, Guid Id)
        //{

        //    var Schemes = GetAllCustomers(filter, Id);
        //    if (Schemes.Any())
        //    {
        //        return PageList<InsuranceScheme>.ToPagedList(Schemes, filter.PageNumber, filter.PageSize);
        //    }
        //    throw new SchemeNotFoundException("No Scheme Data found");
        //}

        //public List<Agent> GetAllCustomers(Guid id)
        //{
        //    var schemes = _repository.GetAll().Where(x => x.PlanId == id).ToList();
        //    return schemes;
        //}
    }
}
