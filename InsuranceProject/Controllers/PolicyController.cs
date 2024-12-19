using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IInsuranceScheme _schemeService;
        
        public PolicyController(IPolicyService policyService,IInsuranceScheme schemeService, IUserService userService, ICustomerService customerService)
        {
            _policyService = policyService;
            _userService = userService;
            _customerService = customerService;
            _schemeService = schemeService;
        }


        [HttpPost, Authorize(Roles = "AGENT,CUSTOMER")]
        public IActionResult Add(PolicyDto policyDto)
        {
            var newId = _policyService.Add(policyDto);
            return Ok(newId);
        }

        [HttpDelete,Authorize(Roles = "AGENT,CUSTOMER")]
        public IActionResult Delete(Guid id)
        {
            if (_policyService.Delete(id))
            {
                return Ok(id);
            }
            return BadRequest();
        }

        [HttpGet("{id}"),Authorize(Roles = "AGENT,CUSTOMER,ADMIN,EMPLOYEE")]
        public IActionResult Get(Guid id)
        {
            var employee = _policyService.Get(id);
            return Ok(employee);
        }

        [HttpPut, Authorize(Roles = "CUSTOMER,ADMIN")]
        public IActionResult Update(PolicyDto policyDto)
        {
            if (_policyService.Update(policyDto))
                return Ok(policyDto);
            return NotFound("Policy not found");
        }

        [HttpPut("UpdatePolicy"), Authorize(Roles = "CUSTOMER,ADMIN,EMPLOYEE")]
        public IActionResult UpdatePolicy(PolicyDto policyDto)
        {
            if (_policyService.UpdatePolicy(policyDto))
                return Ok(policyDto);
            return NotFound("Policy not found");
        }


        [HttpGet("Policy"),Authorize(Roles = "CUSTOMER,EMPLOYEE,ADMIN,AGENT")]
        public IActionResult GetPolicies([FromQuery] FilterParameter policyFilter, Guid id)
        {
            Console.WriteLine($"Fetching user with ID: {id}");

            var user = _customerService.GetById(id);

            if (user == null)
            {
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

        [HttpGet("get"), Authorize(Roles = "AGENT,CUSTOMER,ADMIN,EMPLOYEE")]
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

        [HttpGet("get2"), Authorize(Roles = "AGENT")]
        public IActionResult GetAlll([FromQuery] FilterParameter filterParameter, [FromQuery] Guid agentId)
        {
            var pagedCustomers = _policyService.GetAlll(filterParameter,agentId);

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
    }
}
