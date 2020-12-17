using System.IO;
using Newtonsoft.Json;

namespace App
{
    public static class ResourcesHelper
    {
        public static string[] GetProxies(string path) =>
            File.ReadAllLines(path);

        public static Question[] GetQuestions(string path) =>
            JsonConvert.DeserializeObject<Question[]>(File.ReadAllText(path));
    }
}
