using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IEmployeeService
    {
        public PagedResult<EmployeeDto> GetAll(FilterParameter filterParameter);
        public Employee GetById(Guid id);
        public Guid AddEmployee(EmployeeRegisterDto employeeRegisterDto);
        public bool DeleteEmployee(Guid id);
        public bool UpdateEmployee(EmployeeDto employeeDto);

        public bool ChangePassword(ChangePasswordDto passwordDto);
    }
}
