using AutoMapper;
using InsuranceProject.Data;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using InsuranceProject.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Numerics;
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
            var scheme = _repository.GetAll().AsNoTracking().FirstOrDefault(x=>x.SchemeName==insuranceSchemeDto.SchemeName&&
            x.PlanId==insuranceSchemeDto.PlanId);
            if (scheme!=null)
            {
                throw new InsurancePlanNotFoundException("Not Found");
            }
            List<DocumentType> Documents = new List<DocumentType>();
            foreach (var doc in insuranceSchemeDto.Requireddocuments)
            {
                if (Enum.TryParse(typeof(DocumentType), doc, true, out var result))
                {
                    Documents.Add((DocumentType)result);
                }
            }
            var insuranceScheme = _mapper.Map<InsuranceScheme>(insuranceSchemeDto);
            insuranceScheme.RequiredDocuments = Documents;
            _repository.Add(insuranceScheme);
            var plan = _planRepository.Get(insuranceSchemeDto.PlanId);
            plan.Schemes.Add(insuranceScheme);
            return insuranceScheme.SchemeId;
        }

        public bool CheckSchemeNameDuplicate(string schemeName)
        {
            // Query the database to check if a scheme with the given name already exists
            var existingScheme = _repository.GetAll().AsNoTracking()
                                               .FirstOrDefault(s => s.SchemeName.ToLower() == schemeName.ToLower());

            return existingScheme != null; // Return true if found, false otherwise
        }

        public bool Delete(Guid id)
        {
            var policy = _repository.Get(id);
            if (policy != null)
            {
                policy.IsActive = false;
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
            var existingPolicy = _repository.GetAll().AsNoTracking().FirstOrDefault(x=>x.SchemeId==insuranceSchemeDto.SchemeId);
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<InsuranceScheme>(insuranceSchemeDto);
                List<DocumentType> Documents = new List<DocumentType>();
                foreach (var doc in insuranceSchemeDto.Requireddocuments)
                {
                    if (Enum.TryParse(typeof(DocumentType), doc, true, out var result))
                    {
                        Documents.Add((DocumentType)result);
                    }
                }
                policy.RequiredDocuments = Documents;
                if (insuranceSchemeDto.IsActive == true)
                {
                    policy.IsDeleted = false;
                }
                _repository.Update(policy);
                return true;
            }
            return false;
        }

        public bool Update2(InsuranceSchemeDto insuranceSchemeDto)
        {
            var existingPlan = _planRepository.GetAll().AsNoTracking().Include(x => x.Schemes).FirstOrDefault(x => x.PlanId == insuranceSchemeDto.PlanId);
            //var existingPolicy = _repository.GetAll().AsNoTracking().FirstOrDefault(x=>x.SchemeId==insuranceSchemeDto.SchemeId && x.IsDeleted==true);
            var existingPolicy = existingPlan?.Schemes
                .FirstOrDefault(x => x.SchemeId == insuranceSchemeDto.SchemeId);
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<InsuranceScheme>(insuranceSchemeDto);
                List<DocumentType> Documents = new List<DocumentType>();
                foreach (var doc in insuranceSchemeDto.Requireddocuments)
                {
                    if (Enum.TryParse(typeof(DocumentType), doc, true, out var result))
                    {
                        Documents.Add((DocumentType)result);
                    }
                }
                policy.RequiredDocuments = Documents;
                if (insuranceSchemeDto.IsActive == true)
                {
                    policy.IsDeleted = false;
                }
                _repository.Update(policy);
                return true;
            }
            return false;
        }

        public PagedResult<InsuranceScheme> GetAllScheme(FilterParameter filter, Guid planId)
        {
            // Retrieve the plan with the specified planId and include its related schemes
            var plan = _planRepository.GetAll()
                .Include(x => x.Schemes)
                .FirstOrDefault(x => x.PlanId == planId);

            // If no plan is found, throw an exception
            if (plan == null)
            {
                throw new ArgumentException($"Plan with ID {planId} not found.", nameof(planId));
            }

            // If no schemes are found, throw an exception
            if (plan.Schemes == null)
            {
                throw new ArgumentException($"No schemes found for the plan with ID {planId}.");
            }

            // Filter the schemes based on the status being true
            var query = plan.Schemes.Where(s => s.Status == true);

            // Apply filtering based on other filter parameters (e.g., Name or another field if needed)
            if (!string.IsNullOrEmpty(filter.Name))  // If Name filtering is required, add here
            {
                query = query.Where(s => s.SchemeName.Contains(filter.Name));
            }

            // Calculate total count for pagination metadata
            int totalCount = query.Count();

            // Apply pagination using the filter's PageNumber and PageSize
            var pagedData = query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            // Create the paged result
            var pagedResult = new PagedResult<InsuranceScheme>
            {
                Items = pagedData,
                TotalCount = totalCount,
                PageSize = filter.PageSize,
                CurrentPage = filter.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize),
                HasNext = filter.PageNumber < (int)Math.Ceiling(totalCount / (double)filter.PageSize),
                HasPrevious = filter.PageNumber > 1
            };

            // Return the paginated result
            return pagedResult;
        }

        public PageList<InsuranceSchemeDto> GetAll(FilterParameter filter, Guid planId)
        {
            // Call GetAllScheme to get the paginated list of InsuranceSchemes
            var pagedSchemes = GetAllScheme(filter, planId);

            // Map the InsuranceScheme entities to their corresponding DTOs
            var schemeDtos = _mapper.Map<List<InsuranceSchemeDto>>(pagedSchemes.Items);

            // Check if there are any schemes to return
            if (schemeDtos.Any())
            {
                // Return the paginated DTO list
                return PageList<InsuranceSchemeDto>.ToPagedList(schemeDtos, filter.PageNumber, filter.PageSize);
            }

            // If no schemes are found, throw a custom exception
            throw new SchemeNotFoundException("No Scheme Data found");
        }

        public List<InsuranceScheme> GetAllSchemes(Guid id)
        {
            var schemes = _repository.GetAll().Where(x => x.PlanId == id).ToList();
            return schemes;
        }

        public List<InsuranceSchemeDto> GetAllSchemes2(Guid id)
        {
            var schemes = _repository.GetAll().Where(x => x.PlanId == id).ToList();
            var policydtos = _mapper.Map<List<InsuranceSchemeDto>>(schemes);
            return policydtos;
        }
    }
}
