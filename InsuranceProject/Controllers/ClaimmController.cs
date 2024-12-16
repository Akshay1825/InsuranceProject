using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimmController : ControllerBase
    {
        private readonly IClaimService _service;
        public ClaimmController(IClaimService service)
        {
            _service = service;
        }

        [HttpGet("get")]
        public IActionResult GetAll([FromQuery] DateFilter filterParameter)
        {
            var pagedCustomers = _service.GetAll(filterParameter);

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
        public IActionResult Add(ClaimDto claimDto)
        {
            var id = _service.Add(claimDto);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var role = _service.Get(id);
            return Ok(role);
        }
        [HttpPut]
        public IActionResult Update(ClaimDto claimDto)
        {
            if (_service.Update(claimDto))
            {
                return Ok(claimDto);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_service.Delete(id))
            {
                return Ok(id);
            }
            return NotFound();
        }
    }
}
