using InsuranceProject.DTOs;
using InsuranceProject.Models;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxSettingsService _taxService;

        public TaxController(ITaxSettingsService taxService)
        {
            _taxService = taxService;
        }

        [HttpGet, Authorize(Roles = "AGENT,CUSTOMER,ADMIN")]
        public IActionResult Get()
        {
            List<TaxSettings> taxSettings =  _taxService.Get();
            return Ok(taxSettings);
        }

        [HttpPost, Authorize(Roles = "ADMIN")]
        public IActionResult Add(TaxSettings tax)
        {
            var taxId = _taxService.Add(tax);
            return Ok(taxId);
        }

        [HttpPut, Authorize(Roles = "ADMIN")]
        public IActionResult Update(TaxSettings tax)
        {
            if (_taxService.Update(tax))
            {
                return Ok(tax);
            }
            return NotFound();
        }
    }
}
