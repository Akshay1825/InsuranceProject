
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
        private readonly IRepository<Agent> _agentRepository;

        public CommissionService(IRepository<Commission> repository, IRepository<Agent> agentRepository)
        {
            _repository = repository;
            _agentRepository = agentRepository;
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

            var agent = _agentRepository.GetAll().AsNoTracking().FirstOrDefault(x=>x.Id==commission.AgentId);

            if (commission.Status==2)
            {
                var subject = "Commission Approval";
                var roundedAmount = Math.Round(commission.Amount, 2);
                var body = $@"
          <p>Dear {agent.FirstName},</p>
          <p>Your commission has been approved by NewInsurance</p>
          <p>The Details are as below:</p>
          <p>Policy Number : <b>{commission.policyNumber}</b></p>
          <p>Commission Type : <b>{commission.CommissionType}</b></p>
          <p>Commission Amount : <b>{roundedAmount}</b></p>
          <p>In next 2-3 working days amount will be deposited in your registered bank account </p>
          <p>Best regards,<br/>New-Insurance Team</p> ";

                var emailService = new EmailService();
                emailService.SendEmail(agent.Email, subject, body);
            }
            if (commission.Status==3)
            {
                var subject = "Commission Rejected";
                var roundedAmount = Math.Round(commission.Amount, 2);
                var body = $@"
          <p>Dear {agent.FirstName},</p>
          <p>Your commission has been Rejected by NewInsurance</p>
          <p>The Details are as below:</p>
          <p>Policy Number : <b>{commission.policyNumber}</b></p>
          <p>Commission Type : <b>{commission.CommissionType}</b></p>
          <p>Commission Amount : <b>{roundedAmount}</b></p>
          <p>Kindly contact support team for further clarification</p>
          <p>Best regards,<br/>New-Insurance Team</p> ";

                var emailService = new EmailService();
                emailService.SendEmail(agent.Email, subject, body);
            }
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
