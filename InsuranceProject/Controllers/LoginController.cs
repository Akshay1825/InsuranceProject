using InsuranceProject.DTOs;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            var existingUser = _loginService.FindByUserName(loginDto.UserName);
            var token = _loginService.GetUser(loginDto);
            if (token == null) 
            {
                return NotFound();
            }
            Response.Headers.Add("Jwt", token);
            return Ok(new { roleName = existingUser.Role.RoleName });
        }
    }
}
