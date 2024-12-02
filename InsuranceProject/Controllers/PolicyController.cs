using InsuranceProject.DTOs;
using InsuranceProject.Models;
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
    }
}
