using InsuranceProject.DTOs;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customerDTOs = _customerService.GetAll();
            return Ok(customerDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var existingCustomerDTO = _customerService.Get(id);
            return Ok(existingCustomerDTO);
        }

        [HttpPost]
        public IActionResult Add(CustomerDto customerDto)
        {
            var newCustomerId = _customerService.Add(customerDto);
            return Ok(newCustomerId);
        }

        [HttpPut]
        public IActionResult Update(CustomerDto customerDto)
        {
            var UpdatedCustomerDTO = _customerService.Update(customerDto);
            if (UpdatedCustomerDTO != null)
                return Ok(UpdatedCustomerDTO);
            return NotFound("Customer Not Found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(CustomerDto customerDto)
        {
            if (_customerService.Delete(customerDto))
                return Ok("Customer Deleted Successfully");
            return NotFound("Customer Not Found");
        }
    }
}
