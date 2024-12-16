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
        private readonly IInsuranceScheme _schemeService;
        
        public PolicyController(IPolicyService policyService,IInsuranceScheme schemeService, IUserService userService, ICustomerService customerService)
        {
            _policyService = policyService;
            _userService = userService;
            _customerService = customerService;
            _schemeService = schemeService;
        }

        [HttpGet("Policy")]
        public IActionResult GetPolicies([FromQuery] FilterParameter policyFilter, Guid id)
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

        [HttpGet("get2")]
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

        [HttpPost]
        public IActionResult Add(PolicyDto policyDto)
        {
            var newId = _policyService.Add(policyDto);
            var scheme = _schemeService.GetAll().FirstOrDefault(x => x.SchemeId == policyDto.InsuranceSchemeId);
            var customer = _policyService.GetUser(policyDto);


            if (customer!=null)
            {
                var subject = "New-Insurance";
                var body = $@"
          <p>Dear {customer.FirstName},</p>
          <p>Your Policy Has Been Sucessfully Generated.</p>
          <p>Your Documents Have been sent for Verification</p>
          <p>Kindly wait for approval your kyc-status will be updated soon</p>
          <p>Looking forward to working with you. :) </p>
          <p>Best regards,<br/>New-Insurance Team</p> ";

                var emailService = new EmailService();
                emailService.SendEmail(customer.Email, subject, body);
            }
            if (policyDto.AgentId != null)
            {
                var user = _userService.GetById(customer.UserId);

                var subject = "New-Insurance";
                var body = $@"
          <p>Dear {customer.FirstName},</p>
          <p>Your Policy Has Been Sucessfully Generated.</p>
          <p>Kindly Login with the credentials provided before and upload the documents as below:</p>
          <p>Kindly Upload Documents As per mention in the scheme</p>
          <p>Looking forward to working with you. :) </p>
          <p>Best regards,<br/>New-Insurance Team</p> ";

                var emailService = new EmailService();
                emailService.SendEmail(customer.Email, subject, body);
            }
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

        [HttpPut("UpdatePolicy")]
        public IActionResult UpdatePolicy(PolicyDto policyDto)
        {
            if (_policyService.UpdatePolicy(policyDto))
                return Ok(policyDto);
            return NotFound("Policy not found");
        }
    }
}
