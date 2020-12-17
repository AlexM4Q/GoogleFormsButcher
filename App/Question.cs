using System;
using Newtonsoft.Json;

namespace App
{
    public sealed class Question
    {
        private static readonly Random Rnd = new Random();

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("answers")]
        public string[] Answers { get; set; }

        public string GetAnswer() => Answers[Rnd.Next(Answers.Length)];
    }
}
