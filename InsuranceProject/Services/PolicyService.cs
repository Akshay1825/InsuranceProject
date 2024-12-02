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
        private readonly IRepository<Policy> _policyRepository;
        private readonly IMapper _mapper;

        public PolicyService(IRepository<Policy> repository, IMapper mapper)
        {
            _policyRepository = repository;
            _mapper = mapper;
        }
        public Guid Add(PolicyDto policydto)
        {
            var policy = _mapper.Map<Policy>(policydto);
            _policyRepository.Add(policy);
            return policy.Id;
        }

        public bool Delete(Guid id)
        {
            var policy = _policyRepository.Get(id);
            if (policy != null)
            {
                _policyRepository.Delete(policy);
                return true;
            }
            return false;
        }

        public PolicyDto Get(Guid id)
        {
            var policy = _policyRepository.Get(id);
            if (policy != null)
            {
                var policydto = _mapper.Map<PolicyDto>(policy);
                return policydto;
            }
            throw new Exception("No such policy exist");
        }

        public List<PolicyDto> GetAll()
        {
            var policies = _policyRepository.GetAll().ToList();
            var policydtos = _mapper.Map<List<PolicyDto>>(policies);
            return policydtos;
        }

        public bool Update(PolicyDto policydto)
        {
            var existingPolicy = _policyRepository.Get(policydto.Id);
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<Policy>(policydto);
                _policyRepository.Update(policy);
                return true;
            }
            return false;
        }
    }
}
