using System;
using Microsoft.Extensions.Options;
using InterviewApp.Models;

namespace InterviewApp.Services
{
    public class GreetingService : IGreetingService
    {
        //Refactoring: extending the service to use options pattern for configuration settings      

        private readonly GreetingOptions _options;

        public GreetingService(IOptions<GreetingOptions> options)
        {
            _options = options.Value;
        }
        public void Run()
        {
            string greeting = _options.Language switch
            {
                "English" => $"Hello! {_options.Message}",
                "Afrikaans" => $"Hallo! {_options.Message}",
                "Zulu" => $"Sawubona! {_options.Message}",
                _ => $"Hello (default)! {_options.Message}"
            };
            Console.WriteLine(greeting);
        }
    }
}