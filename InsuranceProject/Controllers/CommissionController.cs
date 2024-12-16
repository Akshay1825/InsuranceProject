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
    public class CommissionController : ControllerBase
    {
        private readonly ICommissionService _commissionService;

        public CommissionController(ICommissionService commissionService)
        {
            _commissionService = commissionService;
        }

        [HttpGet("get")]
        public IActionResult GetCommisson([FromQuery]Guid AgentId,[FromQuery] DateFilter dateFilter)
        {

            var commissions = _commissionService.GetAll(AgentId,dateFilter);

            if (commissions.Any())
            {
                var metadata = new
                {
                    commissions.TotalCount,
                    commissions.PageSize,
                    commissions.CurrentPage,
                    commissions.TotalPages,
                    commissions.HasNext,
                    commissions.HasPrevious,
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(commissions);
            }
            return BadRequest("No data found");
        }

        [HttpPut]
        public IActionResult Update(Commission commission)
        {
            if (_commissionService.UpdateCustomer(commission))
            {
                return Ok(commission);
            }
            return NotFound();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll([FromQuery] DateFilter pageParameter)
        {
            var pagedCustomers = _commissionService.GetAll(pageParameter);

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
