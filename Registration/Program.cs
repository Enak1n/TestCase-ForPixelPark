using Microsoft.Extensions.DependencyInjection;
using Registration.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Registration.Services;
using Newtonsoft.Json;
using System;
using Registration.HTTPServer;

namespace SimpleWebServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
               .AddDbContext<Context>(options =>
                   options.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=masj109ia4002"))
               .AddScoped<IEmailService, EmailService>()
               .BuildServiceProvider();

            var emailService = serviceProvider.GetRequiredService<IEmailService>();

            var httpServer = new HttpServer(emailService); // Создание экземпляра HttpServer
            await httpServer.StartListeningAsync(); // Запуск сервера

            Console.WriteLine("HTTP server is now running. Press Enter to stop.");
            Console.ReadLine();
        }
    }
}
