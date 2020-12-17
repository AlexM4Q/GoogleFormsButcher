using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace App
{
    public sealed class Butcher : IDisposable
    {
        public Question[] Questions { get; set; }
        public string FormId { get; set; }
        private HttpClient _client;
        private int _counter;

        public void Dispose() => _client?.Dispose();

        public void Go(string proxy)
        {
            _client = new HttpClient(new HttpClientHandler
            {
                Proxy = new WebProxy
                {
                    Address = new Uri(proxy)
                }
            });

            var uriBuilder = new StringBuilder();
            for (var i = 0; i < 1000; i++)
            {
                uriBuilder.Clear();
                uriBuilder.Append("https://docs.google.com/forms/d/e/");
                uriBuilder.Append(FormId);
                uriBuilder.Append("/formResponse?");

                foreach (var question in Questions)
                {
                    uriBuilder.Append("entry.");
                    uriBuilder.Append(question.Id);
                    uriBuilder.Append('=');
                    uriBuilder.Append(question.GetAnswer());
                    uriBuilder.Append('&');
                }

                var url = new Uri(uriBuilder.ToString(), UriKind.Absolute);
                var response = _client.PostAsync(url, null).GetAwaiter().GetResult();
                Console.WriteLine($"{DateTime.Now:MM.dd.yyyy HH:mm:ss.fff}: {++_counter} {response.StatusCode}");

                // var page = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                // File.WriteAllBytes(@"C:\Users\zhelonkin\Downloads\123.html", page);
                // Thread.Sleep(1000);
            }
        }
    }
}
