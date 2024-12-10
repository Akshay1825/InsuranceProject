using InsuranceProject.Helper;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiumController : ControllerBase
    {
        //private readonly IPremiumService _premiumService;

        //public PremiumController(IPremiumService premiumService)
        //{
        //    _premiumService = premiumService;
        //}

        //// Pay a specific premium
        //[HttpPost("{premiumId}")]
        //public IActionResult PayPremium(Guid premiumId)
        //{
        //    var result = _premiumService.PayPremium(premiumId);
        //    if (!result.Status)
        //        return BadRequest(result.Status);

        //    return Ok(result);
        //}

        //// Get all premiums and their statuses for a policy (for admin)
        //[HttpGet("policy/{policyId}/premiums/status")]
        ////[Authorize(Roles = "Admin")]
        //public IActionResult GetPremiumStatuses(Guid policyId)
        //{
        //    var premiumStatuses = _premiumService.GetPremiumStatuses(policyId);
        //    return Ok(premiumStatuses);
        //}

        //[HttpGet("Account/{id}")]
        ////[Authorize(Roles = "Admin")]
        //public IActionResult GetPremiumByAccount(Guid id, [FromQuery] PageParameter pageParameters)
        //{
        //    var premiums = _premiumService.GetPremiumByPolicyAccount(id, pageParameters);
        //    return Ok(premiums);
        //}
    }
}
