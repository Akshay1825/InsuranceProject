using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
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
            int policyNumber = GenerateUniquePolicyNumber();
            policydto.PolicyNumber = policyNumber;     //Uniquly created Policy number
            var policy = _mapper.Map<Policy>(policydto);
            _policyRepository.Add(policy);
            return policy.PolicyId;
        }

        private int GenerateUniquePolicyNumber()
        {
            int policyNumber;
            bool exists;
            do
            {
                policyNumber = new Random().Next(100000, 999999);
                exists = _policyRepository.Any(p => p.PolicyNumber == policyNumber);

            } while (exists);
            return policyNumber;
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
            var policy = _policyRepository.GetAll().AsNoTracking().FirstOrDefault(x => x.PolicyId == id);
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
            policydto.Payments++;
            var existingPolicy = _policyRepository.GetAll().AsNoTracking().FirstOrDefault(x => x.PolicyId == policydto.PolicyId); ;
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<Policy>(policydto);
                _policyRepository.Update(policy);
                return true;
            }
            return false;
        }

        public PagedResult<PolicyDto> GetPoliciesWithCustomerId(PolicyFilter filterParameter, Guid userID)
        {
            var query = _policyRepository.GetAll().AsNoTracking().Where(x=>x.CustomerId==userID).ToList();
            int totalCount = query.Count();
            var pagedCustomers = query
            .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
            .Take(filterParameter.PageSize)
                .ToList();

            var policyDtos = _mapper.Map<List<PolicyDto>>(pagedCustomers);
            var pagedResult = new PagedResult<PolicyDto>
            {
                Items = policyDtos,
                TotalCount = totalCount,
                PageSize = filterParameter.PageSize,
                CurrentPage = filterParameter.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasNext = filterParameter.PageNumber < (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasPrevious = filterParameter.PageNumber > 1
            };

            return pagedResult;
        }

        public PagedResult<PolicyDto> GetAll(FilterParameter filterParameter)
        {
            var query = _policyRepository.GetAll().AsNoTracking();
            int totalCount = query.Count();
            var pagedCustomers = query
            .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
            .Take(filterParameter.PageSize)
                .ToList();

            var customerDtos = _mapper.Map<List<PolicyDto>>(pagedCustomers);
            var pagedResult = new PagedResult<PolicyDto>
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
    }
}
