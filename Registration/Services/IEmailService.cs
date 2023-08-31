using Registration.Entity;

namespace Registration.Services
{
    public interface IEmailService
    {
        Task <EmailModel> SendCode(string email);
    }
}
