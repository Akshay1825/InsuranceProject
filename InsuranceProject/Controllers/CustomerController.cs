using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Services;
using InsuranceProject.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InsuranceProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IDocumentService _documentService;
        public CustomerController(ICustomerService customerService, IDocumentService documentService)
        {
            _customerService = customerService;
            _documentService = documentService;
        }

        [HttpPost]
        public IActionResult Add(CustomerRegistrationDto customerRegisterDto)
        {
            var id = _customerService.AddCustomer(customerRegisterDto);
            return Ok(id);
        }
        
        [HttpGet("{id}"), Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER,AGENT")]
        public IActionResult Get(Guid id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                throw new CustomerNotFoundException("No such Customer Found");
            }
            return Ok(customer);
        }
        [HttpPut, Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER,AGENT")]
        public IActionResult Update(CustomerDto customerDto)
        {
            if (_customerService.UpdateCustomer(customerDto))
            {
                return Ok(customerDto);
            }
            return NotFound();
        }

        [HttpGet("GetByAgentId"), Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER,AGENT")]
        public IActionResult GetAll(Guid id)
        {
            var customers = _customerService.GetAllCustomers(id);
            return Ok(customers);
        }

        [HttpGet("check-existence")]
        public IActionResult CheckExistence([FromQuery] string userName, [FromQuery] string email, [FromQuery] long mobileNumber)
        {
            var usernameExists = _customerService.GetAlll().Any(c => c.UserName == userName);
            var emailExists = _customerService.GetAlll().Any(c => c.Email == email);
            var mobileExists = _customerService.GetAlll().Any(c => c.MobileNumber == mobileNumber);

            if (usernameExists)
            {
                return BadRequest(new { field = "userName", message = "Username already exists" });
            }

            if (emailExists)
            {
                return BadRequest(new { field = "email", message = "Email already exists" });
            }

            if (mobileExists)
            {
                return BadRequest(new { field = "mobileNumber", message = "Mobile number already exists" });
            }

            return Ok(new { message = "All fields are valid" });
        }

        [HttpGet("check-username"), Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER,AGENT")]
        public IActionResult CheckUsername([FromQuery] string userName)
        {
            var usernameExists = _customerService.GetAlll().Any(c => c.UserName == userName);
            return Ok(new { UsernameExists = usernameExists });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_customerService.DeleteCustomer(id))
            {
                return Ok(id);
            }
            return NotFound();
        }

        [HttpPut("changepassword"), Authorize(Roles = "CUSTOMER")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (_customerService.ChangePassword(changePasswordDto))
            {
                return Ok(changePasswordDto);
            }
            return NotFound("Agent not found");
        }

        [HttpGet("GetByUserName"), Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER,AGENT")]
        public IActionResult GetByUserName([FromQuery] string userName)
        {
            var customer = _customerService.GetByUserName(userName);
            return Ok(new { customer });
        }

        [HttpGet("get"), Authorize(Roles = "ADMIN,EMPLOYEE")]
        public IActionResult GetAll([FromQuery] FilterParameter filterParameter)
        {
            var pagedCustomers = _customerService.GetCustomers(filterParameter);

            var metadata = new
            {
                pagedCustomers.TotalCount,
                pagedCustomers.PageSize,
                pagedCustomers.CurrentPage,
                pagedCustomers.TotalPages,
                pagedCustomers.HasNext,
                pagedCustomers.HasPrevious,
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(pagedCustomers.Items);
        }

        [HttpGet("documents"), Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public IActionResult GetDocuments([FromQuery] PageParameter pageParameter, Guid customerID)
        {
            var pagedCustomers = _documentService.GetByCustomerId(pageParameter,customerID);

            var metadata = new
            {
                pagedCustomers.TotalCount,
                pagedCustomers.PageSize,
                pagedCustomers.CurrentPage,
                pagedCustomers.TotalPages,
                pagedCustomers.HasNext,
                pagedCustomers.HasPrevious,
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(pagedCustomers);

        }

        [HttpGet("complaints"), Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public IActionResult GetComplaints([FromQuery] FilterParameter filterParameter, string userName)

        {
            var user = _customerService.GetByUserName(userName);
            if (user != null)
            {
                var complaints = _customerService.GetCustomerComplaints(user.CustomerId, filterParameter);
                var metadata = new
                {
                    complaints.TotalCount,
                    complaints.PageSize,
                    complaints.CurrentPage,
                    complaints.TotalPages,
                    complaints.HasNext,
                    complaints.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(complaints);
            }
            return BadRequest("Bad request");
        }

       
    }
}
