using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
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
        private readonly IRepository<PolicyAccount> _policyAccountRepository;
        private readonly IMapper _mapper;
        public CustomerService(IRepository<Customer> cutomerRepository, IMapper mapper, IRepository<Role> roleRepository, IRepository<User> userRepository, IRepository<PolicyAccount> policyAccountRepository)
        {
            _mapper = mapper;
            _repository = cutomerRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _policyAccountRepository = policyAccountRepository;
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
            _repository.Add(customer);

            // Ensure CustomerId is valid before returning
            if (customer.CustomerId == Guid.Empty)
            {
                throw new Exception("Failed to generate CustomerId.");
            }

            return customer.CustomerId;
        }
        public Guid AddPolicyAccount(PolicyAccountDto policyAccountDto)
        {
            var policyAccont = _mapper.Map<PolicyAccount>(policyAccountDto);
            _policyAccountRepository.Add(policyAccont);
            var customer = _repository.Get(policyAccont.CustomerId);
            customer.PolicyAccount = policyAccont;
            _repository.Update(customer);
            return policyAccont.Id;
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
                _repository.Delete(customer);
                return true;
            }
            return false;
        }

        public Customer GetById(Guid id)
        {
            return _repository.Get(id);
        }

        public List<CustomerDto> GetCustomers()
        {
            var customer = _repository.GetAll().AsNoTracking().Include(p => p.PolicyAccount).ToList();
            List<CustomerDto> customerDtos = _mapper.Map<List<CustomerDto>>(customer);
            return customerDtos;
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
    }
}
