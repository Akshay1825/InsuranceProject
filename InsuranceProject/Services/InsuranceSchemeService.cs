using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;

namespace InsuranceProject.Services
{
    public class InsuranceSchemeService : IInsuranceScheme
    {
        private readonly IRepository<InsuranceScheme> _repository;
        private readonly IRepository<InsurancePlan> _planRepository;
        private readonly IMapper _mapper;

        public InsuranceSchemeService(IRepository<InsuranceScheme> repository,IMapper mapper, IRepository<InsurancePlan> planRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _planRepository = planRepository;
        }
        public Guid Add(InsuranceSchemeDto insuranceSchemeDto)
        {
            var insuranceScheme = _mapper.Map<InsuranceScheme>(insuranceSchemeDto);
            _repository.Add(insuranceScheme);
            var plan = _planRepository.Get(insuranceSchemeDto.PlanId);
            plan.Schemes.Add(insuranceScheme);
            return insuranceScheme.SchemeId;
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

        public InsuranceSchemeDto Get(Guid id)
        {
            var policy = _repository.Get(id);
            if (policy != null)
            {
                var policydto = _mapper.Map<InsuranceSchemeDto>(policy);
                return policydto;
            }
            throw new Exception("No such Insurance Scheme exist");
        }

        public List<InsuranceSchemeDto> GetAll()
        {
            var policies = _repository.GetAll().ToList();
            var policydtos = _mapper.Map<List<InsuranceSchemeDto>>(policies);
            return policydtos;
        }

        public bool Update(InsuranceSchemeDto insuranceSchemeDto)
        {
            var existingPolicy = _repository.Get(insuranceSchemeDto.SchemeId);
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<InsuranceScheme>(insuranceSchemeDto);
                _repository.Update(policy);
                return true;
            }
            return false;
        }

        public List<InsuranceScheme> GetAllScheme(FilterParameter filter,Guid planId)
        {
            var plan = _planRepository.GetAll().Include(x => x.Schemes).FirstOrDefault(x => x.PlanId == planId);
           
            if (plan == null)
            {
                throw new ArgumentException($"Plan with ID {planId} not found.", nameof(planId));
            }

            if (plan.Schemes == null || !plan.Schemes.Any())
            {
                throw new ArgumentException($"No schemes found for the plan with ID {planId}.");
            }

            var query = plan.Schemes.Where(s => s.Status == true);

            //if (filter.Id != null)
            //{
            //    query = query.Where(s => s.SchemeId == filter.Id);
            //}

            //if (!string.IsNullOrEmpty(filter.Name))
            //{
            //    query = query.Where(s => s.SchemeName.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));
            //}

            //if (!query.Any())
            //{
            //    Console.WriteLine("No schemes matched the filter criteria.");
            //    return new List<InsuranceScheme>(new List<InsuranceScheme>());
            //}

            return new List<InsuranceScheme>(query.ToList());
        }

        public PageList<InsuranceScheme> GetAll(FilterParameter filter, Guid planId)
        {

            var Schemes = GetAllScheme(filter,planId);
            if (Schemes.Any())
            {
                return PageList<InsuranceScheme>.ToPagedList(Schemes, filter.PageNumber, filter.PageSize);
            }
            throw new SchemeNotFoundException("No Scheme Data found");
        }

        public List<InsuranceScheme> GetAllSchemes(Guid id)
        {
            var schemes = _repository.GetAll().Where(x => x.PlanId == id).ToList();
            return schemes;
        }
    }
}
