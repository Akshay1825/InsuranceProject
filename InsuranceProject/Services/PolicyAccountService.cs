using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Models;
using InsuranceProject.Repositories;

namespace InsuranceProject.Services
{
    public class PolicyAccountService
    {
        private readonly IRepository<PolicyAccount> _repository;
        private readonly IMapper _mapper;

        public PolicyAccountService(IRepository<PolicyAccount> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Guid Add(PolicyAccountDto policyAccountDto)
        {
            var policyAccount = _mapper.Map<PolicyAccount>(policyAccountDto);
            _repository.Add(policyAccount);
            return policyAccount.Id;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(PolicyAccountDto policyAccountDto)
        {
            throw new NotImplementedException();
        }
    }
}
