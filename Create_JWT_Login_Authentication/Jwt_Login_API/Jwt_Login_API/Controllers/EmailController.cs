using Jwt_Login_API.Models;
using Jwt_Login_API.Services.EmailService;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace Jwt_Login_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmailController : ControllerBase
    {
        // LinkVideo: https://www.youtube.com/watch?v=PvO_1T0FS_A
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(EmailDto request)
        {
            _emailService.SendMail(request);
            return Ok();
        }

    }
}
