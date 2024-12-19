using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository<Policy> _policyRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<InsuranceScheme> _schemeRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Commission> _commissionRepository;

        public PolicyService(IRepository<Policy> repository,IRepository<Commission> commissionRepository,IRepository<User> userRepository, IMapper mapper,IRepository<Customer> customerRepository,IRepository<InsuranceScheme> insuranceScheme)
        {
            _policyRepository = repository;
            _mapper = mapper;
            _schemeRepository = insuranceScheme;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _commissionRepository = commissionRepository;
        }
        public Guid Add(PolicyDto policydto)
        {
            int policyNumber = GenerateUniquePolicyNumber();
            policydto.PolicyNumber = policyNumber;

            var insuranceScheme = _schemeRepository.GetAll().AsNoTracking().FirstOrDefault(x=>x.SchemeId==policydto.InsuranceSchemeId);
            var commissionPercent = insuranceScheme.RegistrationCommRatio;
            
            var policy = _mapper.Map<Policy>(policydto);
            policy.Status = 0;
            _policyRepository.Add(policy);
            if (policydto.AgentId != null)
            {
                var commission = new Commission()
                {
                    CommissionId = new Guid(),
                    AgentId = policydto.AgentId,
                    Date = policydto.IssueDate,
                    CommissionType = "Registration",
                    policyNumber = policydto.PolicyNumber,
                    PolicyId = policy.PolicyId,
                    Amount = (policydto.InvestmentAmount * (commissionPercent / 100)),
                    Status = 1
                };
                _commissionRepository.Add(commission);
            }

            var scheme = _schemeRepository.GetAll().FirstOrDefault(x => x.SchemeId == policydto.InsuranceSchemeId);
            var customer = _customerRepository.Get(policydto.CustomerId);

            if (customer != null)
            {
                var subject = "New-Insurance";
                var body = $@"
          <p>Dear {customer.FirstName},</p>
          <p>Your Policy Has Been Sucessfully Generated.</p>
          <p>Your Documents Have been sent for Verification</p>
          <p>Kindly wait for approval your E-KYC status will be updated soon</p>
          <p>Looking forward to working with you. :) </p>
          <p>Best regards,<br/>New-Insurance Team</p> ";

                var emailService = new EmailService();
                emailService.SendEmail(customer.Email, subject, body);
            }
            if (policydto.AgentId != null)
            {
                var user = _userRepository.Get(customer.UserId);

                var subject = "New-Insurance";
                var body = $@"
          <p>Dear {customer.FirstName},</p>
          <p>Your Policy Has Been Sucessfully Generated.</p>
          <p>Kindly Login with the credentials provided before and upload the documents as below:</p>
          <p>Kindly Upload Documents As per mention in the scheme</p>
          <p>Looking forward to working with you. :) </p>
          <p>Best regards,<br/>New-Insurance Team</p> ";

                var emailService = new EmailService();
                emailService.SendEmail(customer.Email, subject, body);
            }
            return policy.PolicyId;
        }

        private int GenerateUniquePolicyNumber()
        {
            int policyNumber;
            bool exists;
            do
            {
                policyNumber = new Random().Next(100000, 999999);
                exists = _policyRepository.Any(p => p.PolicyNumber == policyNumber);

            } while (exists);
            return policyNumber;
        }

        public Customer GetUser(PolicyDto policyDto)
        {
            var customer = _customerRepository.GetAll().AsNoTracking().FirstOrDefault(x=>x.CustomerId==policyDto.CustomerId);
            return customer;
        }

        public bool Delete(Guid id)
        {
            var policy = _policyRepository.Get(id);
            if (policy != null)
            {
                _policyRepository.Delete(policy);
                return true;
            }
            return false;
        }

        public PolicyDto Get(Guid id)
        {
            var policy = _policyRepository.GetAll().AsNoTracking().FirstOrDefault(x => x.PolicyId == id);
            if (policy != null)
            {
                var policydto = _mapper.Map<PolicyDto>(policy);
                return policydto;
            }
            throw new Exception("No such policy exist");
        }

        public List<PolicyDto> GetAll()
        {
            var policies = _policyRepository.GetAll().ToList();
            var policydtos = _mapper.Map<List<PolicyDto>>(policies);
            return policydtos;
        }

        public bool Update(PolicyDto policydto)
        {
            policydto.Payments++;
            var existingPolicy = _policyRepository.GetAll().AsNoTracking().FirstOrDefault(x => x.PolicyId == policydto.PolicyId); ;
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<Policy>(policydto);
                _policyRepository.Update(policy);
                return true;
            }
            return false;
        }

        public bool UpdatePolicy(PolicyDto policydto)
        {
            var existingPolicy = _policyRepository.GetAll().AsNoTracking().FirstOrDefault(x => x.PolicyId == policydto.PolicyId); ;
            
            var customer = _customerRepository.GetAll().AsNoTracking().FirstOrDefault(x=>x.CustomerId == policydto.CustomerId);
            
            if(policydto.Status==1)
            {
                var subject = "E-KYC Successful";
                var roundedAmount = Math.Round(policydto.PremiumAmount, 2);
                var body = $@"
          <p>Dear {customer.FirstName},</p>
          <p>Your E-KYC has been Successfully Approved by NewInsurance</p>
          <p>The Details are as below:</p>
          <p>Policy Number : <b>{policydto.PolicyNumber}</b></p>
          <p>Scheme Name : <b>{policydto.SchemeName}</b></p>
          <p>Premium Amount : <b>{roundedAmount}</b></p>
          <p>Kindly Pay Premiums on Time </p>
          <p>Best regards,<br/>New-Insurance Team</p> ";

                var emailService = new EmailService();
                emailService.SendEmail(customer.Email, subject, body);
            }

            if (policydto.Status == 2)
            {
                var subject = "E-KYC Rejected";
                var roundedAmount = Math.Round(policydto.PremiumAmount, 2);
                var body = $@"
          <p>Dear {customer.FirstName},</p>
          <p>Your E-KYC has been Rejected by NewInsurance</p>
          <p>The Details are as below:</p>
          <p>Policy Number : <b>{policydto.PolicyNumber}</b></p>
          <p>Scheme Name : <b>{policydto.SchemeName}</b></p>
          <p>Premium Amount : <b>{roundedAmount}</b></p>
          <p>Kindly Contact Support team for further clarification</p>
          <p>Best regards,<br/>New-Insurance Team</p> ";

                var emailService = new EmailService();
                emailService.SendEmail(customer.Email, subject, body);
            }

            if (existingPolicy != null)
            {
                var policy = _mapper.Map<Policy>(policydto);
                _policyRepository.Update(policy);
                return true;
            }
            return false;
        }

        public PagedResult<PolicyDto> GetPoliciesWithCustomerId(FilterParameter filterParameter, Guid userID)
        {
            // Fetch all policies for the specific customer ID
            var query = _policyRepository.GetAll()
                                         .AsNoTracking()
                                         .Where(x => x.CustomerId == userID);

            // Apply filtering based on the filter parameter (e.g., Name)
            if (!string.IsNullOrEmpty(filterParameter.Name))
            {
                query = query.Where(x => x.SchemeName.Contains(filterParameter.Name));
            }

            // Calculate total count for pagination metadata
            int totalCount = query.Count();

            // Apply pagination
            var pagedPolicies = query
                .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
                .Take(filterParameter.PageSize)
                .ToList();

            // Map to DTOs using AutoMapper
            var policyDtos = _mapper.Map<List<PolicyDto>>(pagedPolicies);

            // Create the paginated result
            var pagedResult = new PagedResult<PolicyDto>
            {
                Items = policyDtos,
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



        public PagedResult<PolicyDto> GetAll(FilterParameter filterParameter)
        {
            var query = _policyRepository.GetAll().AsNoTracking();
            int totalCount = query.Count();
            var pagedCustomers = query
            .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
            .Take(filterParameter.PageSize)
                .ToList();

            var customerDtos = _mapper.Map<List<PolicyDto>>(pagedCustomers);
            var pagedResult = new PagedResult<PolicyDto>
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

        public PagedResult<PolicyDto> GetAlll(FilterParameter filterParameter,Guid Id)
        {
            var query = _policyRepository.GetAll().AsNoTracking().Where(x=>x.AgentId==Id);

            // Apply filtering based on the filter parameter (e.g., Name)
            if (!string.IsNullOrEmpty(filterParameter.Name))
            {
                query = query.Where(c => c.SchemeName.Contains(filterParameter.Name));
            }

            // Calculate total count for pagination metadata
            int totalCount = query.Count();

            // Apply pagination
            var pagedData = query
                .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
                .Take(filterParameter.PageSize)
                .ToList();

            // Map to DTOs using AutoMapper
            var customerDtos = _mapper.Map<List<PolicyDto>>(pagedData);

            // Create the paged result
            var pagedResult = new PagedResult<PolicyDto>
            {
                Items = customerDtos,
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
    }
}
