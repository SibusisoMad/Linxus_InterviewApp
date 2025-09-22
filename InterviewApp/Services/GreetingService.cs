using InterviewApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace InterviewApp.Services
{
    public class GreetingService : IGreetingService
    {
       
        private readonly GreetingOptions _options;
        private readonly ILogger<GreetingService> _logger;

        public GreetingService(IOptions<GreetingOptions> options, ILogger<GreetingService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }
        public void Run()
        {
            _logger.LogInformation("GreetingService starting...");

            try
            {
                string greeting;

                greeting = _options.Language switch
                {
                    "English" => $"Hello! {_options.Message}",
                    "Afrikaans" => $"Hallo! {_options.Message}",
                    "Zulu" => $"Sawubona! {_options.Message}",
                    _ => GetUnsupportedGreeting(_options.Language, _options.Message)
                };


                if (string.IsNullOrEmpty(_options.Language))
                {
                    _logger.LogDebug("Language is not provided please check appsettings configuration");
                    return;
                }

                if (string.IsNullOrEmpty(_options.Message))
                {
                    _logger.LogError("Message is not provided please check appsettings configuration");
                    return; 
                }

                Console.WriteLine(greeting);
                _logger.LogInformation("{Greeting}", greeting);


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
