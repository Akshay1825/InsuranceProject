using InsuranceProject.DTOs;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchemeDetailsController : ControllerBase
    {
        private readonly ISchemeDetailsService _service;
        public SchemeDetailsController(ISchemeDetailsService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rolesDto = _service.GetAll();
            return Ok(rolesDto);
        }

        [HttpPost]
        public IActionResult Add(SchemeDetailsDto schemeDetailsDto)
        {
            var id = _service.Add(schemeDetailsDto);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var role = _service.Get(id);
            return Ok(role);
        }
        [HttpPut]
        public IActionResult Update(SchemeDetailsDto schemeDetailsDto)
        {
            if (_service.Update(schemeDetailsDto))
            {
                return Ok(schemeDetailsDto);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_service.Delete(id))
            {
                return Ok(id);
            }
            return NotFound();
        }
    }
}
