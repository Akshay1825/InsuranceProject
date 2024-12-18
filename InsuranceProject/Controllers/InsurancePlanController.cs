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
    public class InsurancePlanController : ControllerBase
    {
        private readonly IInsurancePlanService _service;
        public InsurancePlanController(IInsurancePlanService service)
        {
            _service = service;
        }

        
        [HttpPost, Authorize(Roles = "ADMIN")]
        public IActionResult Add(InsurancePlanDto insurancePlanDto)
        {
            var existing = _service.GetByUserName(insurancePlanDto);
            if (existing != null)
            {
                return NotFound();
            }
            var id = _service.Add(insurancePlanDto);
            return Ok(id);
        }

        [HttpGet("{id}"), Authorize(Roles = "ADMIN,EMPLOYEE,AGENT,CUSTOMER")]
        public IActionResult Get(Guid id)
        {
            var role = _service.Get(id);
            return Ok(role);
        }
        [HttpPut, Authorize(Roles = "ADMIN")]
        public IActionResult Update(InsurancePlanDto insurancePlanDto)
        {
            if (_service.Update(insurancePlanDto))
            {
                return Ok(insurancePlanDto);
            }
            return NotFound();
        }

        [HttpDelete("{id}"),Authorize(Roles ="ADMIN")]
        public IActionResult Delete(Guid id)
        {
            if (_service.Delete(id))
            {
                return Ok(id);
            }
            return NotFound();
        }

        [HttpGet("get"), Authorize(Roles = "ADMIN,EMPLOYEE,AGENT,CUSTOMER")]
        public IActionResult GetAll([FromQuery] FilterParameter filterParameter)
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

        [HttpGet("get2"), Authorize(Roles = "ADMIN,EMPLOYEE,AGENT,CUSTOMER")]
        public IActionResult GetAlll([FromQuery] FilterParameter filterParameter)
        {
            var pagedCustomers = _service.GetAlll(filterParameter);

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
