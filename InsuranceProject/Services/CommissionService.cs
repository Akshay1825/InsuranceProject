
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InsuranceProject.Services
{
    public class CommissionService:ICommissionService
    {
        private readonly IRepository<Commission> _repository;

        public CommissionService(IRepository<Commission> repository)
        {
            _repository = repository;
        }
        public PageList<Commission> GetAll(Guid AgentId, DateFilter dateFilter)
        {
            // Get the list of commissions for the given agent
            var query = _repository.GetAll()
                                   .Where(x => x.AgentId == AgentId)
                                   .AsNoTracking()
                                   .ToList();

            // Apply date filter if both FromDate and ToDate are provided
            if (dateFilter.FromDate.HasValue && dateFilter.ToDate.HasValue)
            {
                query = query.Where(c => c.Date >= dateFilter.FromDate.Value &&
                                          c.Date <= dateFilter.ToDate.Value).ToList();
            }

            // If any data is found, apply pagination and return the paginated list
            if (query.Any())
            {
                // Use the PageList helper to paginate the results
                return PageList<Commission>.ToPagedList(query, dateFilter.PageNumber, dateFilter.PageSize);
            }

            // Throw an exception if no data was found
            throw new DataNotFoundException("No Commission Data found for the specified criteria");
        }




        public bool UpdateCustomer(Commission commission)
        {
            var existingCustomer = _repository.GetAll().AsNoTracking().Where(u => u.CommissionId == commission.CommissionId);
            if (existingCustomer != null)
            {
                _repository.Update(commission);
                return true;
            }
            return false;
        }

        public PageList<Commission> GetAll(DateFilter dateFilter)
        {
            // Get all commissions without tracking
            var query = _repository.GetAll().AsNoTracking().ToList();

            // Apply date filtering if both FromDate and ToDate are provided
            if (dateFilter.FromDate.HasValue && dateFilter.ToDate.HasValue)
            {
                query = query.Where(c => c.Date >= dateFilter.FromDate.Value &&
                                          c.Date <= dateFilter.ToDate.Value).ToList();
            }

            // If data is found, paginate and return the result
            if (query.Any())
            {
                return PageList<Commission>.ToPagedList(query, dateFilter.PageNumber, dateFilter.PageSize);
            }

            // If no data is found, throw an exception
            throw new DocumentNotFoundException("No data found for the given date filter");
        }

    }
}
