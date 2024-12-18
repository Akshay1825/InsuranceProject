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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;
        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet, Authorize(Roles = "ADMIN")]
        public IActionResult GetAll()
        {
            var rolesDto = _service.GetAll();
            return Ok(rolesDto);
        }

        [HttpPost, Authorize(Roles = "CUSTOMER")]
        public IActionResult Add(PaymentDto paymentDto)
        {
            var id = _service.Add(paymentDto);
            return Ok(id);
        }

        [HttpGet("{id}"), Authorize(Roles = "ADMIN,EMPLOYEE,AGENT,CUSTOMER")]
        public IActionResult Get(Guid id)
        {
            var role = _service.Get(id);
            return Ok(role);
        }
        [HttpPut, Authorize(Roles = "ADMIN")]
        public IActionResult Update(PaymentDto paymentDto)
        {
            if (_service.Update(paymentDto))
            {
                return Ok(paymentDto);
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

        [HttpGet("GetID"),Authorize(Roles = "ADMIN,EMPLOYEE,CUSTOMER")]
        public IActionResult GetID([FromQuery]int index, [FromQuery]Guid policyId)
        {
            var payment = _service.GetID(index,policyId);
            return Ok(payment);
        }

        [HttpGet("getAll"), Authorize(Roles = "ADMIN,EMPLOYEE,AGENT,CUSTOMER")]
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

            return Ok(pagedCustomers);
        }
    }
}
