using InterviewApp.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewApp.Commands
{
    public class GreetUserHandler : IRequestHandler<GreetUserCommand, string>
    {
        private readonly IGreetingService _greetingService;
        private readonly ILogger<GreetUserHandler> _logger;

        public GreetUserHandler(IGreetingService greetingService, ILogger<GreetUserHandler> logger)
        {
            _greetingService = greetingService;
            _logger = logger;
        }

        public async Task<string> Handle(GreetUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GreetUserCommand...");

            string message = await _greetingService.GetGreetingMessageAsync();

            return message;
        }
    }
}
