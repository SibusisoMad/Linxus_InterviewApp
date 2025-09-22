using InterviewApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewApp.Services
{
    public class GreetingService : IGreetingService
    {
       
        private readonly GreetingOptions _options;
        private readonly ILogger<GreetingService> _logger;
        private readonly ITimeGreetingService _timeGreetingService;

        public GreetingService(IOptions<GreetingOptions> options, ILogger<GreetingService> logger, ITimeGreetingService timeGreetingService)
        {
            _options = options.Value;
            _logger = logger;
            _timeGreetingService = timeGreetingService;
        }
        public void Run()
        {
            _logger.LogInformation("GreetingService starting...");

            try
            {
                string greetingMsg;

                greetingMsg = _options.Language switch
                {
                    "English" => $"Hello! {_options.Message}",
                    "Afrikaans" => $"Hallo! {_options.Message}",
                    "Zulu" => $"Sawubona! {_options.Message}",
                    _ => GetUnsupportedGreeting(_options.Language, _options.Message)
                };

                var currentTimeGreeting = _timeGreetingService.GetTimeBasedGreeting();
                greetingMsg = $"{currentTimeGreeting} {greetingMsg}";

                string greetingMessage = greetingMsg;



                var requiredFields = new List<string>();

                if (string.IsNullOrEmpty(_options.Message))
                    requiredFields.Add(nameof(_options.Message));

                if (string.IsNullOrEmpty(_options.Language))
                    requiredFields.Add(nameof(_options.Language));

                if (requiredFields.Any())
                {
                    _logger.LogError("The following required fields are missing or empty in appsettings.json: {MissingFields}",
                        string.Join(", ", requiredFields)
                    );
                    return;
                }

                Console.WriteLine(greetingMessage);
                _logger.LogInformation("{Greeting}", greetingMessage);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service encounter erros while running..");
            }
        }


            private string GetUnsupportedGreeting(string language, string message)
        {
            _logger.LogWarning("Unsupported language '{Language}' received. Falling back to default.", language ?? "null");
            return $"Hello (default)! {message}";
        }
    }
    }
