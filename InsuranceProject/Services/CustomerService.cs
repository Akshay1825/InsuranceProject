using AutoMapper;
using InsuranceProject.Data;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class CustomerService : ICustomerService
    {
        private Guid _roleId = new Guid("74c70ef9-b3f4-4e6d-50bb-08dd115dd6c7");
        private readonly IRepository<Customer> _repository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Agent> _agentRepository;
        private readonly IMapper _mapper;
        public CustomerService(IRepository<Customer> cutomerRepository,IRepository<Agent> agentRepository,IMapper mapper, IRepository<Role> roleRepository, IRepository<User> userRepository)
        {
            _mapper = mapper;
            _repository = cutomerRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _agentRepository = agentRepository;
        }

        public Guid AddCustomer(CustomerRegistrationDto customerRegistrationDto)
        {
            // Create user
            var user = new User
            {
                RoleId = _roleId,
                UserName = customerRegistrationDto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(customerRegistrationDto.Password),
                Status = true
            };
            _userRepository.Add(user);

            // Retrieve and validate role
            var role = _roleRepository.Get(_roleId);
            if (role == null)
            {
                throw new Exception($"Role with ID {_roleId} not found.");
            }

            role.Users ??= new List<User>(); // Ensure Users is initialized
            role.Users.Add(user);

            // Map and save customer
            customerRegistrationDto.UserId = user.Id;
            var customer = _mapper.Map<Customer>(customerRegistrationDto);

            if (customerRegistrationDto.AgentId != null)
            {
                var agent = _agentRepository.GetAll().FirstOrDefault(x => x.Id == customerRegistrationDto.AgentId);
                agent.CustomerCount++;

            }
            _repository.Add(customer);

            // Ensure CustomerId is valid before returning
            if (customer.CustomerId == Guid.Empty)
            {
                throw new Exception("Failed to generate CustomerId.");
            }

            return customer.CustomerId;
        }
        
        public bool ChangePassword(ChangePasswordDto passwordDto)
        {
            var customer = _repository.GetAll().AsNoTracking().Include(a => a.User).Where(a => a.User.UserName == passwordDto.UserName).FirstOrDefault();
            if (customer != null)
            {
                if (BCrypt.Net.BCrypt.Verify(passwordDto.Password, customer.User.PasswordHash))
                {
                    customer.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordDto.NewPassword);
                    _repository.Update(customer);
                    return true;
                }

            }
            return false;
        }

        public bool DeleteCustomer(Guid id)
        {
            var customer = _repository.Get(id);
            if (customer != null)
            {
                if (customer.AgentId!=null)
                {
                    var agent = _agentRepository.GetAll().FirstOrDefault(x => x.Id == customer.AgentId);
                    agent.CustomerCount--;
                }
                _repository.Delete(customer);
                return true;
            }
            return false;
        }

        public Customer GetById(Guid id)
        {
            return _repository.Get(id);
        }

        public PagedResult<CustomerDto> GetCustomers(FilterParameter filterParameter)
        {
            var query = _repository.GetAll();

            // Apply the name filter
            if (!string.IsNullOrEmpty(filterParameter.Name))
            {
                query = query.Where(c => c.FirstName.Contains(filterParameter.Name));
            }

            // Calculate total count for pagination metadata
            var totalCount = query.Count();

            // Apply pagination
            var customers1 = query
                .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
                .Take(filterParameter.PageSize)
                .ToList();

            var customers = _mapper.Map<List<CustomerDto>>(customers1);

            // Return paginated result
            return new PagedResult<CustomerDto>
            {
                Items = customers,
                TotalCount = totalCount,
                PageSize = filterParameter.PageSize,
                CurrentPage = filterParameter.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasNext = filterParameter.PageNumber < (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasPrevious = filterParameter.PageNumber > 1
            };
        }
    

        public bool UpdateCustomer(CustomerDto customerDto)
        {
            var existingCustomer = _repository.GetAll().AsNoTracking().Where(u => u.CustomerId == customerDto.CustomerId);
            if (existingCustomer != null)
            {
                var customer = _mapper.Map<Customer>(customerDto);
                _repository.Update(customer);
                return true;
            }
            return false;
        }

        public Customer GetByUserName(string userName)
        {
            var customer = _repository.GetAll().AsNoTracking().FirstOrDefault(u => u.UserName == userName);
            return customer;
        }

        public PageList<Complaint> GetCustomerComplaints(Guid userID, FilterParameter filterParameter)
        {
            var customer = _repository.GetAll().Include(u => u.Complaints).AsNoTracking().FirstOrDefault(x => x.CustomerId == userID);
            //if (customer == null)
            //{
            //    throw new CustomerNotFoundException("Customer Not Found");
            //    //return new PageList<Complaint>();
            //}
            //else if (filterParameter.Id != null)
            //{
            //    return PageListcustomer.Complaints.FindAll(q => q.Status == true && q.ComplaintId == filterParameter.Id).ToList();
            //}
            //else if (filterParameter.Name != null)
            //{
            //    return customer.Queries.FindAll(q => q.Status == true && q.ComplaintName.Contains(filterParameter.Name)).ToList();
            //}
            var queries = customer.Complaints.FindAll(q => q.Status == true).ToList();
            return PageList<Complaint>.ToPagedList(queries, filterParameter.PageNumber, filterParameter.PageSize);
        }

        public PageList<Customer> GetAll(FilterParameter filter, Guid planId)
        {

            var Schemes = GetAllSchemes(planId,filter);

            
            if (Schemes.Any())
            {
                return PageList<Customer>.ToPagedList(Schemes, filter.PageNumber, filter.PageSize);
            }
            throw new SchemeNotFoundException("No Scheme Data found");
        }

        public List<Customer> GetAllSchemes(Guid id,FilterParameter filterParameter)
        {
            var customer = _repository.GetAll().Where(x => x.AgentId == id).ToList();

            if (!string.IsNullOrWhiteSpace(filterParameter.Name))
            {
                customer = customer.Where(x => x.FirstName.Contains(filterParameter.Name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return customer;
        }

        public List<Customer> GetAllCustomers(Guid id)
        {
            var customers = _repository.GetAll().Where(x=>x.AgentId == id).ToList();
            return customers;
        }

        public List<Customer> GetAlll()
        {
            var customers = _repository.GetAll();
            return customers.ToList();
        }
    }
}
