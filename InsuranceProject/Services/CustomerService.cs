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
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Guid Add(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _repository.Add(customer);
            return customer.CustomerId;
        }

        public bool Delete(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            var existingCustomer = _repository.GetById(customer.CustomerId);
            if (existingCustomer != null)
            {
                _repository.Delete(existingCustomer);
                return true;
            }
            return false;
        }

        public CustomerDto Get(Guid id)
        {
            var customer = _repository.GetById(id);
            if (customer == null)
            {
                throw new CustomerNotFoundException("Customer Not Found");
            }
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }

        public List<CustomerDto> GetAll()
        {
            var customers = _repository.GetAll().ToList();
            List<CustomerDto> result = _mapper.Map<List<CustomerDto>>(customers);
            return result;
        }

        public CustomerDto Update(CustomerDto customerDto)
        {
            var existingCustomer = _mapper.Map<Customer>(customerDto);
            var updatedCustomer = _repository.GetAll().AsNoTracking().FirstOrDefault(x => x.CustomerId == existingCustomer.CustomerId);
            if (updatedCustomer != null)
            {
                _repository.Update(updatedCustomer);
            }
            var updatedCustomerDto = _mapper.Map<CustomerDto>(updatedCustomer);
            return updatedCustomerDto;
        }
    }
}
