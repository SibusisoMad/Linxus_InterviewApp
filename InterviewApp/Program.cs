using InterviewApp.Commands;
using InterviewApp.Models;
using InterviewApp.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                
                services.Configure<GreetingOptions>(context.Configuration.GetSection("Greeting"));

               
                var greetingsDict = context.Configuration.GetSection("Greetings")
                .Get<Dictionary<string, GreetingLanguageOptions>>();
                services.Configure<GreetingsConfig>(options => options.Languages = greetingsDict);

               
                services.AddTransient<IGreetingService, GreetingService>();
                services.AddSingleton<ITimeGreetingService, TimeGreetingService>();

              
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GreetUserHandler>());
            })
            .Build();

        
        var mediator = host.Services.GetRequiredService<IMediator>();

       
        string greetingMessage = await mediator.Send(new GreetUserCommand());

        if (!string.IsNullOrEmpty(greetingMessage))
            Console.WriteLine(greetingMessage);

        await host.RunAsync();
    }
}
