using System.Collections.Generic;

namespace InterviewApp.Models
{
    public class GreetingsConfig
    {
        public Dictionary<string, GreetingLanguageOptions> Languages { get; set; } = new();
    }
}
