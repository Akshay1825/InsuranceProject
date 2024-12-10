using InsuranceProject.DTOs;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IPolicyService _policyService;

        public AdminController(IAdminService adminService, IPolicyService policyService)
        {
            _adminService = adminService;
            _policyService = policyService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var admins = _adminService.GetAll();
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var admin = _adminService.Get(id);
            return Ok(admin);
        }

        [HttpPost]
        public IActionResult Add(AdminRegisterDto adminRegisterDto)
        {
            var adminId = _adminService.Add(adminRegisterDto);
            return Ok(adminId);
        }

        [HttpPost("Policy")]
        public IActionResult Add(PolicyDto policyDto)
        {
            var policyId = _policyService.Add(policyDto);
            return Ok(policyId);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_adminService.Delete(id))
                return Ok(id);
            return NotFound("Admin Not Found");
        }

        [HttpPut]
        public IActionResult Update(AdminDto adminDto)
        {
            if (_adminService.Update(adminDto))
                return Ok(adminDto);
            return NotFound("Admin not found");
        }

        [HttpGet("getProfile")]
        public IActionResult GetByUserName(string userName)
        {
            var admin = _adminService.GetByUserName(userName);
            return Ok(admin);
        }

        [HttpPut("changepassword")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (_adminService.ChangePassword(changePasswordDto))
            {
                return Ok(changePasswordDto);
            }
            return NotFound("Admin not found");
        }
    }
}
