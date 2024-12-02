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
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userDtos = _userService.GetUsers();
            return Ok(userDtos);
        }

        [HttpPost]
        public IActionResult Add(UserDto userDto)
        {
            var id = _userService.AddUser(userDto);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }
        [HttpPut]
        public IActionResult Update(UserDto userDto)
        {
            if (_userService.UpdateUser(userDto))
            {
                return Ok(userDto);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_userService.DeleteUser(id))
            {
                return Ok(id);
            }
            return NotFound();
        }
    }
}
