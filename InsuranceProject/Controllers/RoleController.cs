using InsuranceProject.DTOs;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roleDTOs = _roleService.GetAll();
            return Ok(roleDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var existingRoleDTO = _roleService.Get(id);
            return Ok(existingRoleDTO);
        }

        [HttpPost]
        public IActionResult Add(RoleDto roleDto)
        {
            var newRoleId = _roleService.Add(roleDto);
            return Ok(newRoleId);
        }

        [HttpPut]
        public IActionResult Update(RoleDto roleDto)
        {
            var UpdatedRoleDTO = _roleService.Update(roleDto);
            if (UpdatedRoleDTO != null)
                return Ok(UpdatedRoleDTO);
            return NotFound("Role Not Found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(RoleDto roleDto)
        {
            if (_roleService.Delete(roleDto))
                return Ok("Role Deleted Successfully");
            return NotFound("Role Not Found");
        }
    }
}
