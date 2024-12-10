using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        public PolicyController(IPolicyService policyService, IUserService userService, ICustomerService customerService)
        {
            _policyService = policyService;
            _userService = userService;
            _customerService = customerService;
        }

        [HttpGet("Policy")]
        public IActionResult GetPolicies([FromQuery] PolicyFilter policyFilter, Guid id)
        {
            Console.WriteLine($"Fetching user with ID: {id}");

            var user = _customerService.GetById(id);

            if (user == null)
            {
                // Log if user is not found
                Console.WriteLine($"No customer found with ID: {id}");
                return NotFound(new { message = "Customer not found." });
            }

            if (user != null)
            {
                var pagedPolicies = _policyService.GetPoliciesWithCustomerId(policyFilter, user.CustomerId);

                var metadata = new
                {
                    pagedPolicies.TotalCount,
                    pagedPolicies.PageSize,
                    pagedPolicies.CurrentPage,
                    pagedPolicies.TotalPages,
                    pagedPolicies.HasNext,
                    pagedPolicies.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(pagedPolicies.Items);
            }
            return BadRequest(new { message = "No Policies Found" });
        }

        [HttpGet("get")]
        public IActionResult GetAll([FromQuery] FilterParameter filterParameter)
        {
            var pagedCustomers = _policyService.GetAll(filterParameter);

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

        [HttpPost]
        public IActionResult Add(PolicyDto policyDto)
        {
            var newId = _policyService.Add(policyDto);
            return Ok(newId);
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (_policyService.Delete(id))
            {
                return Ok(id);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var employee = _policyService.Get(id);
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult Update(PolicyDto policyDto)
        {
            if (_policyService.Update(policyDto))
                return Ok(policyDto);
            return NotFound("Policy not found");
        }
    }
}
