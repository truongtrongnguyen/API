using Jwt_Login_API.Models;

namespace Jwt_Login_API.Services.EmailService
{
    public interface IEmailService
    {
        void SendMail(EmailDto email);
    }
}
