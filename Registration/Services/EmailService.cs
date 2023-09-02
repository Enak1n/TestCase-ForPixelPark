using Microsoft.EntityFrameworkCore;
using Registration.Data;
using Registration.Entity;
using System.Text.RegularExpressions;

namespace Registration.Services
{
    public class EmailService : IEmailService
    {
        private readonly Context _context;
        private readonly string _pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$"; // Regex for email

        public EmailService(Context context)
        {
            _context = context;
        }

        public async Task<bool> CheckCode(string code, string email)
        {
            var user = await _context.Emails.OrderBy(x => x.Id).LastOrDefaultAsync(x => x.Email == email);

            if (user != null)
            {
                return user.Code == code;
            }

            return false;
        }

        public async Task<EmailModel> SendCode(string email)
        {
            Guid guid = Guid.NewGuid();
            string formattedGuid = guid.ToString("N").Substring(0, 6);

            if (Regex.IsMatch(email, _pattern))
            {
                var data = new EmailModel(email, formattedGuid);
                await Task.Delay(3000); // Imaginary delay
                /**
                  * We can add real sending email with ﻿System.Net.Mail or with another library
                **/
                await _context.Emails.AddAsync(data);
                await _context.SaveChangesAsync();

                return data;
            }

            return null;
        }
    }
}
