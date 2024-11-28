using InsuranceProject.DTOs;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userDTOs = _userService.GetAll();
            return Ok(userDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var existingUserDTO = _userService.Get(id);
            return Ok(existingUserDTO);
        }

        [HttpPost]
        public IActionResult Add(UserDto userDto)
        {
            var newUserId = _userService.Add(userDto);
            return Ok(newUserId);
        }

        [HttpPut]
        public IActionResult Update(UserDto userDto)
        {
            var UpdatedUserDTO = _userService.Update(userDto);
            if (UpdatedUserDTO != null)
                return Ok(UpdatedUserDTO);
            return NotFound("User Not Found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(UserDto userDto)
        {
            if (_userService.Delete(userDto))
                return Ok("User Deleted Successfully");
            return NotFound("User Not Found");
        }
    }
}
