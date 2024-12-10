using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InsuranceProject.Services
{
    public class InsurancePlanService : IInsurancePlanService
    {
        private readonly IRepository<InsurancePlan> _repository;
        private readonly IMapper _mapper;

        public InsurancePlanService(IRepository<InsurancePlan> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Guid Add(InsurancePlanDto insurancePlanDto)
        {
            var insurancePlan = _mapper.Map<InsurancePlan>(insurancePlanDto);
            _repository.Add(insurancePlan);
            return insurancePlan.PlanId;
        }

        public bool Delete(Guid id)
        {
            var policy = _repository.Get(id);
            if (policy != null)
            {
                _repository.Delete(policy);
                return true;
            }
            return false;
        }

        public InsurancePlanDto Get(Guid id)
        {
            var policy = _repository.Get(id);
            if (policy != null)
            {
                var policydto = _mapper.Map<InsurancePlanDto>(policy);
                return policydto;
            }
            throw new Exception("No such Insurance plan exist");
        }

        public PagedResult<InsurancePlanDto> GetAll(FilterParameter filterParameter)
        {
            var query = _repository.GetAll().AsNoTracking();
            int totalCount = query.Count();
            var pagedCustomers = query
            .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
            .Take(filterParameter.PageSize)
                .ToList();

            var customerDtos = _mapper.Map<List<InsurancePlanDto>>(pagedCustomers);
            var pagedResult = new PagedResult<InsurancePlanDto>
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

        public bool Update(InsurancePlanDto insurancePlanDto)
        {
            var existingPolicy = _repository.Get(insurancePlanDto.PlanId);
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<InsurancePlan>(insurancePlanDto);
                _repository.Update(policy);
                return true;
            }
            return false;
        }
    }
}
