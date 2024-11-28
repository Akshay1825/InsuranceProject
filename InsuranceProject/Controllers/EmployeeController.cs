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
            var employeeDTOs = _employeeService.GetAll();
            return Ok(employeeDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var existingEmployeeDTO = _employeeService.Get(id);
            return Ok(existingEmployeeDTO);
        }

        [HttpPost]
        public IActionResult Add(EmployeeDto employeeDto)
        {
            var newEmployeeId = _employeeService.Add(employeeDto);
            return Ok(newEmployeeId);
        }

        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            var UpdatedEmployeeDTO = _employeeService.Update(employeeDto);
            if (UpdatedEmployeeDTO != null)
                return Ok(UpdatedEmployeeDTO);
            return NotFound("Employee Not Found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(EmployeeDto employeeDto)
        {
            if (_employeeService.Delete(employeeDto))
                return Ok("Employee Deleted Successfully");
            return NotFound("Employee Not Found");
        }

    }
}
