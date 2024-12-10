using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly CaptchaService _captchaService;

        public CaptchaController()
        {
            _captchaService = new CaptchaService();
        }

        [HttpGet]
        public IActionResult GetCaptcha()
        {
            var captchaText = _captchaService.GenerateCaptchaText();
            HttpContext.Session.SetString("Captcha", captchaText); // Store for verification

            var captchaImage = _captchaService.GenerateCaptchaImage(captchaText);
            return File(captchaImage, "image/png");
        }
    }
}
