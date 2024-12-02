using InsuranceProject.DTOs;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface ICustomerService
    {
        public List<CustomerDto> GetCustomers();
        public Customer GetById(Guid id);
        public Guid AddCustomer(CustomerRegistrationDto customerRegistrationDto);
        public bool DeleteCustomer(Guid id);
        public bool UpdateCustomer(CustomerDto customerDto);
        public bool ChangePassword(ChangePasswordDto passwordDto);
        public Guid AddPolicyAccount(PolicyAccountDto policyAccountDto);
    }
}
