using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface ICustomerService
    {
        public PagedResult<CustomerDto> GetCustomers(FilterParameter filterParameter);
        public Customer GetById(Guid id);
        public Guid AddCustomer(CustomerRegistrationDto customerRegistrationDto);
        public bool DeleteCustomer(Guid id);
        public bool UpdateCustomer(CustomerDto customerDto);
        public bool ChangePassword(ChangePasswordDto passwordDto);
        public Customer GetByUserName(string userName);

        public PageList<Complaint> GetCustomerComplaints(Guid userID, FilterParameter filterParameter);

        public PageList<Customer> GetAll(FilterParameter filter, Guid planId);

        public List<Customer> GetAllSchemes(Guid id,FilterParameter filterParameter);

        public List<Customer> GetAllCustomers(Guid id);

        public List<Customer> GetAlll();
    }
}
