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

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var adminDTOs = _adminService.GetAll();
            return Ok(adminDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var existingAdminDTO = _adminService.Get(id);
            return Ok(existingAdminDTO);
        }

        [HttpPost]
        public IActionResult Add(AdminDto adminDto)
        {
            var newAdminId = _adminService.Add(adminDto);
            return Ok(newAdminId);
        }

        [HttpPut]
        public IActionResult Update(AdminDto adminDto)
        {
            var UpdatedAdminDTO = _adminService.Update(adminDto);
            if (UpdatedAdminDTO != null)
                return Ok(UpdatedAdminDTO);
            return NotFound("Admin Not Found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(AdminDto adminDto)
        {
            if (_adminService.Delete(adminDto))
                return Ok("Admin Deleted Successfully");
            return NotFound("Admin Not Found");
        }
    }
}
