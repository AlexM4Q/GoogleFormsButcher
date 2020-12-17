using System;
using System.IO;

namespace App
{
    internal static class Program
    {
        private static void Main()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var proxiesPath = Path.Combine(baseDirectory, "proxies.txt");
            var questionsPath = Path.Combine(baseDirectory, "questions.json");

            var proxies = ResourcesHelper.GetProxies(proxiesPath);
            var questions = ResourcesHelper.GetQuestions(questionsPath);

            var butcher = new Butcher
            {
                Questions = questions,
                FormId = "1FAIpQLSetnwyIqmFVnZH4Svc2Rf7rPOebYjFEj9EjKYwgEBYqZ_jm0g"
            };

            while (true)
            {
                foreach (var proxy in proxies)
                {
                    try
                    {
                        butcher.Go(proxy);
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e);
                        butcher.Dispose();
                    }
                }
            }
        }
    }
}
