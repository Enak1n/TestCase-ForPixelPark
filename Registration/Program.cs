﻿using Microsoft.Extensions.DependencyInjection;
using Registration.Data;
using Microsoft.EntityFrameworkCore;
using Registration.Services;
using Registration.HTTPServer;

namespace SimpleWebServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
               .AddDbContext<Context>(options =>
                   options.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=masj109ia4002")) // Connect to Db.Store in environment variables
               .AddScoped<IEmailService, EmailService>()
               .BuildServiceProvider();

            var emailService = serviceProvider.GetRequiredService<IEmailService>();

            var httpServer = new HttpServer(emailService);
            await httpServer.StartListeningAsync();
        }
    }
}
