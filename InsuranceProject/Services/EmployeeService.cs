using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<Employee> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<EmployeeDto> GetAll()
        {
            var employees = _repository.GetAll().ToList();
            List<EmployeeDto> result = _mapper.Map<List<EmployeeDto>>(employees);
            return result;
        }

        public EmployeeDto Get(Guid id)
        {
            var employee = _repository.GetById(id);
            if (employee == null)
            {
                throw new EmployeeNotFoundException("Employee Not Found");
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public Guid Add(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            _repository.Add(employee);
            return employee.EmployeeId;
        }

        public EmployeeDto Update(EmployeeDto employeeDto)
        {
            var existingEmployee = _mapper.Map<Employee>(employeeDto);
            var updatedEmployee = _repository.GetAll().AsNoTracking().FirstOrDefault(x => x.EmployeeId == existingEmployee.EmployeeId);
            if (updatedEmployee != null)
            {
                _repository.Update(updatedEmployee);
            }
            var updatedEmployeeDto = _mapper.Map<EmployeeDto>(updatedEmployee);
            return updatedEmployeeDto;
        }

        public bool Delete(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var existingEmployee = _repository.GetById(employee.EmployeeId);
            if (existingEmployee != null)
            {
                _repository.Delete(existingEmployee);
                return true;
            }
            return false;
        }
    }
}
