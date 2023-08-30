using Microsoft.Extensions.DependencyInjection;
using Registration.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Registration.Services;
using Newtonsoft.Json;

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

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                if (request.HttpMethod == "GET" && request.Url.AbsolutePath == "/sendcode")
                {
                    var email = request.QueryString.Get("email");

                    // Вызываем метод SendCode
                    var newData = await emailService.SendCode(email);
                    
                    Console.WriteLine($"{newData.DateTime} {newData.Email} код {newData.Code}");

                    response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }

                response.Close();
            }
        }
    }
}
