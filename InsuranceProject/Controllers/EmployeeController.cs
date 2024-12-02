using InsuranceProject.DTOs;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employeeDtos = _employeeService.GetEmployees();
            return Ok(employeeDtos);
        }

        [HttpPost]
        public IActionResult Add(EmployeeRegisterDto employeeRegisterDto)
        {
            var id = _employeeService.AddEmployee(employeeRegisterDto);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var employee = _employeeService.GetById(id);
            return Ok(employee);
        }
        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            if (_employeeService.UpdateEmployee(employeeDto))
            {
                return Ok(employeeDto);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_employeeService.DeleteEmployee(id))
            {
                return Ok(id);
            }
            return NotFound();
        }

    }
}
