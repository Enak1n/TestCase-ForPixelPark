using Microsoft.EntityFrameworkCore;
using Registration.Data;
using Registration.Entity;

namespace Registration.Services
{
    public class EmailService : IEmailService
    {
        private readonly Context _context;

        public EmailService(Context context)
        {
            _context = context;
        }

        public async Task<List<EmailModel>> GetAll()
        {
            return await _context.Emails.ToListAsync();
        }

        public async Task<EmailModel> SendCode(string email)
        {
            var random = new Random();
            string code = random.Next(100000, 1000000).ToString();

            var data = new EmailModel(email, code);

            await _context.Emails.AddAsync(data);
            await _context.SaveChangesAsync();

            return data;
        }
    }
}
