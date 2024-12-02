using InsuranceProject.DTOs;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IEmployeeService
    {
        public List<EmployeeDto> GetEmployees();
        public Employee GetById(Guid id);
        public Guid AddEmployee(EmployeeRegisterDto employeeRegisterDto);
        public bool DeleteEmployee(Guid id);
        public bool UpdateEmployee(EmployeeDto employeeDto);
    }
}
