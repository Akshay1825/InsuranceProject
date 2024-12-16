using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InsuranceProject.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IRepository<Complaint> _repository;
        private readonly IMapper _mapper;

        public ComplaintService(IRepository<Complaint> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Guid Add(ComplaintDto complaintDto)
        {
            var complaint = _mapper.Map<Complaint>(complaintDto);
            _repository.Add(complaint);
            return complaint.ComplaintId;
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

        public ComplaintDto Get(Guid id)
        {
            var policy = _repository.Get(id);
            if (policy != null)
            {
                var policydto = _mapper.Map<ComplaintDto>(policy);
                return policydto;
            }
            throw new Exception("No such complaint exist");
        }

        public PagedResult<ComplaintDto> GetAll(DateFilter filterParameter)
        {
            var query = _repository.GetAll().AsNoTracking().ToList();

            // Apply Date Filtering if provided
            if (filterParameter.FromDate.HasValue && filterParameter.ToDate.HasValue)
            {
                query = query.Where(c => c.DateOfComplaint >= filterParameter.FromDate.Value &&
                                          c.DateOfComplaint <= filterParameter.ToDate.Value).ToList();
            }

            // Get the total count after applying the filters
            int totalCount = query.Count();

            // Apply Pagination
            var pagedComplaints = query
                .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
                .Take(filterParameter.PageSize)
                .ToList();

            // Map to DTOs
            var complaintDtos = _mapper.Map<List<ComplaintDto>>(pagedComplaints);

            // Prepare the paged result
            var pagedResult = new PagedResult<ComplaintDto>
            {
                Items = complaintDtos,
                TotalCount = totalCount,
                PageSize = filterParameter.PageSize,
                CurrentPage = filterParameter.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasNext = filterParameter.PageNumber < (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasPrevious = filterParameter.PageNumber > 1
            };

            return pagedResult;
        }


        public bool Update(ComplaintDto complaintDto)
        {
            var existingPolicy = _repository.GetAll().AsNoTracking().FirstOrDefault(x=>x.ComplaintId== complaintDto.ComplaintId);
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<Complaint>(complaintDto);
                _repository.Update(policy);
                return true;
            }
            return false;
        }
    }
}
