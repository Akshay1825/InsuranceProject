using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository<Policy> _repository;
        private readonly IMapper _mapper;

        public PolicyService(IRepository<Policy> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Guid Add(PolicyDto policyDto)
        {
            var policy = _mapper.Map<Policy>(policyDto);
            _repository.Add(policy);
            return policy.PolicyId;
        }

        public bool Delete(PolicyDto policyDto)
        {
            var policy = _mapper.Map<Policy>(policyDto);
            var existingPolicy = _repository.GetById(policy.PolicyId);
            if (existingPolicy != null)
            {
                _repository.Delete(existingPolicy);
                return true;
            }
            return false;
        }

        public PolicyDto Get(Guid id)
        {
            var policy = _repository.GetById(id);
            if (policy == null)
            {
                throw new PolicyNotFoundException("Policy Not Found");
            }
            var policyDto = _mapper.Map<PolicyDto>(policy);
            return policyDto;
        }

        public List<PolicyDto> GetAll()
        {
            var policies = _repository.GetAll().ToList();
            List<PolicyDto> result = _mapper.Map<List<PolicyDto>>(policies);
            return result;
        }

        public PolicyDto Update(PolicyDto policyDto)
        {
            var existingPolicy = _mapper.Map<Policy>(policyDto);
            var updatedPolicy = _repository.GetAll().AsNoTracking().FirstOrDefault(x => x.PolicyId == existingPolicy.PolicyId);
            if (updatedPolicy != null)
            {
                _repository.Update(updatedPolicy);
            }
            var updatedPolicyDto = _mapper.Map<PolicyDto>(updatedPolicy);
            return updatedPolicyDto;
        }
    }
}
