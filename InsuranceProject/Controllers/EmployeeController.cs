﻿using InsuranceProject.DTOs;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost,Authorize(Roles ="ADMIN")]
        public IActionResult Add(EmployeeRegisterDto employeeRegisterDto)
        {
            var id = _employeeService.AddEmployee(employeeRegisterDto);
            return Ok(id);
        }

        [HttpGet("{id}"),Authorize(Roles = "ADMIN")]
        public IActionResult Get(Guid id)
        {
            var employee = _employeeService.GetById(id);
            return Ok(employee);
        }
        [HttpPut,Authorize(Roles = "ADMIN,EMPLOYEE")]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            if (_employeeService.UpdateEmployee(employeeDto))
            {
                return Ok(employeeDto);
            }
            return NotFound();
        }

        [HttpDelete("{id}"), Authorize(Roles = "ADMIN")]
        public IActionResult Delete(Guid id)
        {
            if (_employeeService.DeleteEmployee(id))
            {
                return Ok(id);
            }
            return NotFound();
        }

        [HttpPut("changepassword"),Authorize(Roles ="EMPLOYEE")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (_employeeService.ChangePassword(changePasswordDto))
            {
                return Ok(changePasswordDto);
            }
            return NotFound("Agent not found");
        }

        [HttpGet("getProfile"), Authorize(Roles = "ADMIN,EMPLOYEE")]
        public IActionResult GetByUserName([FromQuery] string userName)
        {
            var customer = _employeeService.GetByUserName(userName);
            return Ok(new { customer });
        }

        [HttpGet("get"), Authorize(Roles = "ADMIN")]
        public IActionResult GetAll([FromQuery] FilterParameter filterParameter)
        {
            var pagedCustomers = _employeeService.GetAll(filterParameter);

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
