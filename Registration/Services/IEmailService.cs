using Registration.Entity;

namespace Registration.Services
{
    public interface IEmailService
    {
        Task<List<EmailModel>> GetAll();
        Task <EmailModel> SendCode(string email);
    }
}
