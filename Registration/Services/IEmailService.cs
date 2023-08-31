using Registration.Entity;

namespace Registration.Services
{
    public interface IEmailService
    {
        Task <EmailModel> SendCode(string email);
        Task<bool> CheckCode(string code, string email);
    }
}
