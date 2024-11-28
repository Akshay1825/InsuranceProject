using InsuranceProject.DTOs;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var policyDTOs = _policyService.GetAll();
            return Ok(policyDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var existingPolicyDTO = _policyService.Get(id);
            return Ok(existingPolicyDTO);
        }

        [HttpPost]
        public IActionResult Add(PolicyDto policyDto)
        {
            var newPolicyId = _policyService.Add(policyDto);
            return Ok(newPolicyId);
        }

        [HttpPut]
        public IActionResult Update(PolicyDto policyDto)
        {
            var UpdatedPolicyDTO = _policyService.Update(policyDto);
            if (UpdatedPolicyDTO != null)
                return Ok(UpdatedPolicyDTO);
            return NotFound("Policy Not Found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(PolicyDto policyDto)
        {
            if (_policyService.Delete(policyDto))
                return Ok("Policy Deleted Successfully");
            return NotFound("Policy Not Found");
        }
    }
}
