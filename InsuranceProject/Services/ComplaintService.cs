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

        public PagedResult<ComplaintDto> GetAll(FilterParameter filterParameter)
        {
            var query = _repository.GetAll().AsNoTracking();
            int totalCount = query.Count();
            var pagedCustomers = query
            .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
            .Take(filterParameter.PageSize)
                .ToList();

            var customerDtos = _mapper.Map<List<ComplaintDto>>(pagedCustomers);
            var pagedResult = new PagedResult<ComplaintDto>
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

        public bool Update(ComplaintDto complaintDto)
        {
            var existingPolicy = _repository.Get(complaintDto.ComplaintId);
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
