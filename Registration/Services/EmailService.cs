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

        public async Task<bool> CheckCode(string code, string email)
        {
            var user = await _context.Emails.FirstOrDefaultAsync(x => x.Email == email);

            if (user != null)
            {
                return user.Code == code;
            }

            return false;
        }

        public async Task<EmailModel> SendCode(string email)
        {
            var random = new Random();
            string code = random.Next(100000, 1000000).ToString();

            var data = new EmailModel(email, code);

            /**
              * We can add real sending email with ﻿System.Net.Mail or with another library
            **/

            await _context.Emails.AddAsync(data);
            await _context.SaveChangesAsync();

            return data;
        }
    }
}
