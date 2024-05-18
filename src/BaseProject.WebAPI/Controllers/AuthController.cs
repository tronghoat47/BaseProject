using BaseProject.Domain.Constants;
using BaseProject.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.WebAPI.Controllers
{
    [ApiController]
    [Route("auths")]
    public class AuthController : Controller
    {
        private readonly IEmailService _emailService;

        public AuthController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("test-send-email/{email}")]
        public async Task<IActionResult> TestSendEmail(string email)
        {
            var result = await _emailService.SendEmailAsync(email, EmailConstants.SUBJECT_RESET_PASSWORD);
            return Ok(result);
        }
    }
}