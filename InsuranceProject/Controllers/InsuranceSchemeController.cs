using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceSchemeController : ControllerBase
    {
        private readonly IInsuranceScheme _insuranceSchemeService;

        private readonly IInsurancePlanService _insurancePlanService;
        public InsuranceSchemeController(IInsuranceScheme service, IInsurancePlanService insurancePlanService)
        {
            _insuranceSchemeService = service;
            _insurancePlanService = insurancePlanService;
        }

        [HttpGet("getById"), Authorize(Roles = "ADMIN,EMPLOYEE,AGENT,CUSTOMER")]
        public IActionResult GetAll([FromQuery] Guid id)
        {
            var schemes = _insuranceSchemeService.GetAllSchemes2(id);
            return Ok(schemes);
        }

        [HttpGet, Authorize(Roles = "ADMIN,EMPLOYEE,AGENT,CUSTOMER")]
        public IActionResult GetAll()
        {
            var rolesDto = _insuranceSchemeService.GetAll();
            return Ok(rolesDto);
        }

        [HttpPost, Authorize(Roles = "ADMIN")]
        public IActionResult Add(InsuranceSchemeDto insuranceSchemeDto)
        {
            var id = _insuranceSchemeService.Add(insuranceSchemeDto);
            return Ok(id);
        }

        [HttpGet("{id}"), Authorize(Roles = "ADMIN,EMPLOYEE,AGENT,CUSTOMER")]
        public IActionResult Get(Guid id)
        {
            var role = _insuranceSchemeService.Get(id);
            return Ok(role);
        }
        [HttpPut, Authorize(Roles = "ADMIN")]
        public IActionResult Update(InsuranceSchemeDto insuranceSchemeDto)
        {
            if (_insuranceSchemeService.Update(insuranceSchemeDto))
            {
                return Ok(insuranceSchemeDto);
            }
            return NotFound();
        }

        [HttpPut("Update"), Authorize(Roles = "ADMIN")]
        public IActionResult UpdateScheme(InsuranceSchemeDto insuranceSchemeDto)
        {
            if (_insuranceSchemeService.Update2(insuranceSchemeDto))
            {
                return Ok(insuranceSchemeDto);
            }
            return NotFound();
        }

        [HttpGet("check-scheme-name"), Authorize(Roles = "ADMIN")]
        public IActionResult CheckSchemeNameDuplicate(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(new { message = "Scheme name is required" });
            }
            try
            {
                var exists = _insuranceSchemeService.CheckSchemeNameDuplicate(name);
                return Ok(new {exists });
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while checking the scheme name" });
            }
        }

        [HttpDelete("{id}"), Authorize(Roles = "ADMIN")]
        public IActionResult Delete(Guid id)
        {
            if (_insuranceSchemeService.Delete(id))
            {
                return Ok(id);
            }
            return NotFound();
        }

        [HttpGet("getId"), Authorize(Roles = "ADMIN,EMPLOYEE,AGENT,CUSTOMER")]
        public IActionResult GetAll([FromQuery] FilterParameter filterParameter,[FromQuery]Guid id)
        {
            var pagedCustomers = _insuranceSchemeService.GetAll(filterParameter, id);

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
