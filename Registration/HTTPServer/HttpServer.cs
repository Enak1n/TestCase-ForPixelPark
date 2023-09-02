using Registration.Services;
using System.Net;

namespace Registration.HTTPServer
{
    public class HttpServer
    {
        private readonly IEmailService _emailService;

        private readonly HttpListener _listener;
        private static string url = "http://localhost:8000/"; // Store in environment variables

        public HttpServer(IEmailService emailService)
        {
            _emailService = emailService;

            _listener = new HttpListener();
            _listener.Prefixes.Add(url);
        }

        public async Task StartListeningAsync()
        {
            _listener.Start();

            while (true)
            {
                HttpListenerContext context = await _listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                if (request.HttpMethod == "GET" && request.Url.AbsolutePath == "/sendcode")
                {
                    var email = request.QueryString.Get("email");

                    var newData = await _emailService.SendCode(email);

                    if (newData == null)
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                    else
                    {
                        Console.WriteLine($"{newData.DateTime} {newData.Email} код {newData.Code}");

                        response.StatusCode = (int)HttpStatusCode.OK;
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

                if (request.HttpMethod == "GET" && request.Url.AbsolutePath == "/checkCode")
                {
                    var email = request.QueryString.Get("email");
                    var code = request.QueryString.Get("code");

                    var status = await _emailService.CheckCode(code, email);

                    if (status)
                        response.StatusCode = (int)HttpStatusCode.OK;
                    else
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

                response.Headers.Add("Access-Control-Allow-Origin", "*");
                response.Close();
            }
        }
    }
}
