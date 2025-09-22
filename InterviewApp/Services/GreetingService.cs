using InterviewApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewApp.Services
{
    public class GreetingService : IGreetingService
    {
        private readonly ITimeGreetingService _timeGreetingService;
        private readonly GreetingOptions _options;
        private readonly ILogger<GreetingService> _logger;

        public GreetingService(
            ITimeGreetingService timeGreetingService,
            ILogger<GreetingService> logger,
            IOptions<GreetingOptions> options)
        {
            _timeGreetingService = timeGreetingService;
            _logger = logger;
            _options = options.Value;

            _logger.LogInformation("GreetingService initialized.");
        }

        public async Task<string> GetGreetingMessageAsync()
        {
            var missingFields = new List<string>();
            if (string.IsNullOrEmpty(_options.Language))
                missingFields.Add(nameof(_options.Language));
            if (string.IsNullOrEmpty(_options.Message))
                missingFields.Add(nameof(_options.Message));

            if (missingFields.Count > 0)
            {
                _logger.LogError("Missing required configuration: {Fields}", string.Join(", ", missingFields));
                return string.Empty;
            }

            try
            {
                var currentTimeGreeting = await _timeGreetingService.GetTimeBasedGreetingAsync();
                string greetingMessage = $"{currentTimeGreeting} {_options.Message}";

                _logger.LogInformation("Generated greeting: {Greeting}", greetingMessage);

                return greetingMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating greeting");
                return ex.Message;
            }
        }
    }
}
