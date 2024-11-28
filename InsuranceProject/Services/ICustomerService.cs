using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface ICustomerService
    {
        public List<CustomerDto> GetAll();
        public CustomerDto Get(Guid id);
        public Guid Add(CustomerDto customerDto);
        public CustomerDto Update(CustomerDto customerDto);
        public bool Delete(CustomerDto customerDto);
    }
}
