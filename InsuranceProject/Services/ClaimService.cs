using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public PagedResult<ClaimDto> GetAll(DateFilter dateFilter)
        {
            var query = _repository.GetAll().AsNoTracking().ToList();

            // Apply Date Filtering based on FromDate and ToDate
            if (dateFilter.FromDate.HasValue && dateFilter.ToDate.HasValue)
            {
                query = query.Where(c => c.ClaimDate >= dateFilter.FromDate.Value &&
                                          c.ClaimDate <= dateFilter.ToDate.Value).ToList();
            }

            // Get the total count after applying the date filters
            int totalCount = query.Count();

            // Apply Pagination
            var pagedClaims = query
                .Skip((dateFilter.PageNumber - 1) * dateFilter.PageSize)
                .Take(dateFilter.PageSize)
                .ToList();

            // Map to DTOs
            var claimDtos = _mapper.Map<List<ClaimDto>>(pagedClaims);

            // Prepare the paged result
            var pagedResult = new PagedResult<ClaimDto>
            {
                Items = claimDtos,
                TotalCount = totalCount,
                PageSize = dateFilter.PageSize,
                CurrentPage = dateFilter.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)dateFilter.PageSize),
                HasNext = dateFilter.PageNumber < (int)Math.Ceiling(totalCount / (double)dateFilter.PageSize),
                HasPrevious = dateFilter.PageNumber > 1
            };

            return pagedResult;
        }


        public bool Update(ClaimDto claimDto)
        {
            var existingPolicy = _repository.GetAll().AsNoTracking().FirstOrDefault(x=>x.ClaimId==claimDto.ClaimId);
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
