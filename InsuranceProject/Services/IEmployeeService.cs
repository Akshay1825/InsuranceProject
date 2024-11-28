using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface IEmployeeService
    {
        public List<EmployeeDto> GetAll();
        public EmployeeDto Get(Guid id);
        public Guid Add(EmployeeDto employeeDto);
        public EmployeeDto Update(EmployeeDto employeeDto);
        public bool Delete(EmployeeDto employeeDto);
    }
}
