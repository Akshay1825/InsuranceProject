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
            var query = _repository.GetAll().AsNoTracking().Where(x=>x.Status==true);

            if (!string.IsNullOrEmpty(filterParameter.Name))
            {
                query = query.Where(p => p.PlanName.Contains(filterParameter.Name));
            }

            int totalCount = query.Count();

            var pagedPlans = query
                .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
                .Take(filterParameter.PageSize)
                .ToList();

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

            return pagedResult;
        }

        public PagedResult<InsurancePlanDto> GetAlll(FilterParameter filterParameter)
        {
            var query = _repository.GetAll().AsNoTracking();

            if (!string.IsNullOrEmpty(filterParameter.Name))
            {
                query = query.Where(p => p.PlanName.Contains(filterParameter.Name));
            }

            int totalCount = query.Count();

            var pagedPlans = query
                .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
                .Take(filterParameter.PageSize)
                .ToList();

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

            return pagedResult;
        }

        public bool Update(InsurancePlanDto insurancePlanDto)
        {
            var existingPolicy = _repository.GetAll().AsNoTracking().FirstOrDefault(x=>x.PlanId== insurancePlanDto.PlanId);
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<InsurancePlan>(insurancePlanDto);
                //if(policy.Status==true)
                //    policy.Status = false;
                //else policy.Status = true;
                _repository.Update(policy);
                return true;
            }
            return false;
        }

        public InsurancePlan GetByUserName(InsurancePlanDto insurancePlanDto)
        {
            var plan = _repository.GetAll().AsNoTracking().FirstOrDefault(x => x.PlanName == insurancePlanDto.PlanName && x.Status==true);
            return plan;
        }
    }
}
