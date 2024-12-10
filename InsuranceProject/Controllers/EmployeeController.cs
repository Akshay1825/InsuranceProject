﻿using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Services;
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

        [HttpGet("get")]
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

        [HttpPost]
        public IActionResult Add(EmployeeRegisterDto employeeRegisterDto)
        {
            var id = _employeeService.AddEmployee(employeeRegisterDto);
            var subject = "Account Created - New-Insurance";
            var body = $@"
          <p>Dear {employeeRegisterDto.FirstName},</p>
          <p>Your account has been created successfully.</p>
          <p>The below are your Credentials generated by company. Use this to Login into our website.</p>
          <p>Your current Username is: <b>{employeeRegisterDto.UserName}</b></p>
          <p>Your current Password is: <b>{employeeRegisterDto.Password}</b></p>
          <p>If you wish to change your password, please change it after login,in the Profile Section.</p>
          <p>Looking forward to working with you. :) </p>
          <p>Best regards,<br/>New-Insurance Team</p> ";

            var emailService = new EmailService();
            emailService.SendEmail(employeeRegisterDto.Email, subject, body);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var employee = _employeeService.GetById(id);
            return Ok(employee);
        }
        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            if (_employeeService.UpdateEmployee(employeeDto))
            {
                return Ok(employeeDto);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_employeeService.DeleteEmployee(id))
            {
                return Ok(id);
            }
            return NotFound();
        }

        [HttpPut("changepassword")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (_employeeService.ChangePassword(changePasswordDto))
            {
                return Ok(changePasswordDto);
            }
            return NotFound("Agent not found");
        }

    }
}
