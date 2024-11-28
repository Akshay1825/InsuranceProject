using InsuranceProject.DTOs;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var agentDTOs = _agentService.GetAll();
            return Ok(agentDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var existingAgentDTO = _agentService.Get(id);
            return Ok(existingAgentDTO);
        }

        [HttpPost]
        public IActionResult Add(AgentDto agentDto)
        {
            var newAgentId = _agentService.Add(agentDto);
            return Ok(newAgentId);
        }

        [HttpPut]
        public IActionResult Update(AgentDto agentDto)
        {
            var UpdatedAgentDTO = _agentService.Update(agentDto);
            if (UpdatedAgentDTO != null)
                return Ok(UpdatedAgentDTO);
            return NotFound("Agent Not Found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(AgentDto agentDto)
        {
            if (_agentService.Delete(agentDto))
                return Ok("Agent Deleted Successfully");
            return NotFound("Agent Not Found");
        }
    }
}
