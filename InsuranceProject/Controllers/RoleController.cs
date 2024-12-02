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
            var rolesDto = _roleService.GetRoles();
            return Ok(rolesDto);
        }

        [HttpPost]
        public IActionResult Add(RoleDto roleDto)
        {
            var id = _roleService.AddRole(roleDto);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var role = _roleService.GetById(id);
            return Ok(role);
        }
        [HttpPut]
        public IActionResult Update(RoleDto roleDto)
        {
            if (_roleService.UpdateRole(roleDto))
            {
                return Ok(roleDto);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_roleService.DeleteRole(id))
            {
                return Ok(id);
            }
            return NotFound();
        }
    }
}
