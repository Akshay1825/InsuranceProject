using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;
        private readonly ICustomerService _customerService;

        public AgentController(IAgentService agentService, ICustomerService customerService)
        {
            _agentService = agentService;
            _customerService = customerService;
        }

        [HttpGet("get"), Authorize(Roles = "EMPLOYEE,ADMIN")]
        public IActionResult GetAll([FromQuery] FilterParameter filterParameter)
        {
            var (result, metadata) = _agentService.GetAll(filterParameter);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(result.Items);
        }

        [HttpGet("{id}"), Authorize(Roles = "AGENT,EMPLOYEE,ADMIN")]
        public IActionResult Get(Guid id)
        {
            var agent = _agentService.Get(id);
            return Ok(agent);
        }

        [HttpPost,Authorize(Roles = "EMPLOYEE")]
        public IActionResult Add(AgentRegisterDto agentRegisterDto)
        {
            var agentId = _agentService.Add(agentRegisterDto);
            return Ok(agentId);
        }

        [HttpDelete("{id}"),Authorize(Roles ="EMPLOYEE,ADMIN")]
        public IActionResult Delete(Guid id)
        {
            if (_agentService.Delete(id))
                return Ok(id);
            return NotFound("Agent Not Found");
        }

        [HttpPut, Authorize(Roles = "EMPLOYEE,ADMIN,AGENT")]
        public IActionResult Update(AgentDto agentDto)
        {
            if (_agentService.Update(agentDto))
                return Ok(agentDto);
            return NotFound("Agent not found");
        }

        [HttpPut("changepassword"), Authorize(Roles = "AGENT")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (_agentService.ChangePassword(changePasswordDto))
            {
                return Ok(changePasswordDto);
            }
            return NotFound("Agent not found");
        }

        [HttpGet("GetByUserName"), Authorize(Roles = "EMPLOYEE,ADMIN,AGENT,CUSTOMER")]
        public IActionResult GetByUserName([FromQuery] string userName)
        {
            var customer = _agentService.GetByUserName(userName);
            return Ok(new { customer });
        }

        [HttpGet("customers"), Authorize(Roles = "AGENT")]
        public IActionResult GetCustomers([FromQuery] FilterParameter filterParameter, [FromQuery] string userName)
        {
            var user = _agentService.GetByUserName(userName);
            if (user != null)
            {
                var complaints = _customerService.GetAll(filterParameter,user.Id);
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
