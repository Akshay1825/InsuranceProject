using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InsuranceProject.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IRepository<Claimm> _repository;
        private readonly IMapper _mapper;

        public ClaimService(IRepository<Claimm> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Guid Add(ClaimDto claimDto)
        {
            var claim = _mapper.Map<Claimm>(claimDto);
            _repository.Add(claim);
            return claim.ClaimId;
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

        public ClaimDto Get(Guid id)
        {
            var policy = _repository.Get(id);
            if (policy != null)
            {
                var policydto = _mapper.Map<ClaimDto>(policy);
                return policydto;
            }
            throw new Exception("No such claim exist");
        }

        public PagedResult<ClaimDto> GetAll(FilterParameter filterParameter)
        {
            var query = _repository.GetAll().AsNoTracking();
            int totalCount = query.Count();
            var pagedCustomers = query
            .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
            .Take(filterParameter.PageSize)
                .ToList();

            var customerDtos = _mapper.Map<List<ClaimDto>>(pagedCustomers);
            var pagedResult = new PagedResult<ClaimDto>
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

        public bool Update(ClaimDto claimDto)
        {
            var existingPolicy = _repository.Get(claimDto.ClaimId);
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<Claimm>(claimDto);
                _repository.Update(policy);
                return true;
            }
            return false;
        }
    }
}
