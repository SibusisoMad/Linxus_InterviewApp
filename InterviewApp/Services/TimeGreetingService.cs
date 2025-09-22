using InterviewApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewApp.Services
{
    public class TimeGreetingService : ITimeGreetingService
    {
        private readonly ILogger<TimeGreetingService> _logger;
        private readonly GreetingOptions _options;
        private readonly Dictionary<string, GreetingLanguageOptions> _greetings;

        public TimeGreetingService(
            IOptions<GreetingOptions> options,
            IOptions<GreetingsConfig> greetingsConfig,
            ILogger<TimeGreetingService> logger)
        {
            _logger = logger;
            _options = options.Value;
            _greetings = greetingsConfig.Value.Languages;
        }

        public async Task<string> GetTimeBasedGreetingAsync()
        {
            var language = _options.Language;
            if (!_greetings.ContainsKey(language))
            {
                _logger.LogWarning("Unsupported language '{Language}', falling back to English", language);
                language = "English";
            }

            var now = DateTime.Now.TimeOfDay;
            string greeting;

            if (now >= TimeSpan.FromHours(0) && now < TimeSpan.FromHours(12))
                greeting = _greetings[language].Morning;
            else if (now >= TimeSpan.FromHours(12) && now < TimeSpan.FromHours(18))
                greeting = _greetings[language].Afternoon;
            else
                greeting = _greetings[language].Evening;

            _logger.LogInformation("day time greeting: {Greeting}", greeting);

            return await Task.FromResult(greeting);
        }
    }
}
