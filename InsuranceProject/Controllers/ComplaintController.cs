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
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _service;
        public ComplaintController(IComplaintService service)
        {
            _service = service;
        }

        
        [HttpPost, Authorize(Roles = "CUSTOMER")]
        public IActionResult Add(ComplaintDto complaintDto)
        {
            var id = _service.Add(complaintDto);
            return Ok(id);
        }

        [HttpGet("{id}"), Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public IActionResult Get(Guid id)
        {
            var role = _service.Get(id);
            return Ok(role);
        }
        [HttpPut, Authorize(Roles = "EMPLOYEE")]
        public IActionResult Update(ComplaintDto complaintDto)
        {
            if (_service.Update(complaintDto))
            {
                return Ok(complaintDto);
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

        [HttpGet("get"),Authorize(Roles ="ADMIN,EMPLOYEE,CUSTOMER")]
        public IActionResult GetAll([FromQuery] DateFilter dateFilter)
        {
            var pagedCustomers = _service.GetAll(dateFilter);

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
