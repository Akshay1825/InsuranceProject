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
            insurancePlan.Status = true;
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

            // Apply filtering based on the filter parameter (e.g., Name)
            if (!string.IsNullOrEmpty(filterParameter.Name))
            {
                query = query.Where(p => p.PlanName.Contains(filterParameter.Name));
            }

            // Calculate total count for pagination metadata
            int totalCount = query.Count();

            // Apply pagination
            var pagedPlans = query
                .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
                .Take(filterParameter.PageSize)
                .ToList();

            // Map to DTOs using AutoMapper
            var planDtos = _mapper.Map<List<InsurancePlanDto>>(pagedPlans);

            // Create the paged result
            var pagedResult = new PagedResult<InsurancePlanDto>
            {
                Items = planDtos,
                TotalCount = totalCount,
                PageSize = filterParameter.PageSize,
                CurrentPage = filterParameter.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasNext = filterParameter.PageNumber < (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasPrevious = filterParameter.PageNumber > 1
            };

            // Return the paginated result
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

        public InsurancePlan GetByUserName(InsurancePlanDto insurancePlanDto)
        {
            var plan = _repository.GetAll().AsNoTracking().FirstOrDefault(x => x.PlanName == insurancePlanDto.PlanName);
            return plan;
        }
    }
}
