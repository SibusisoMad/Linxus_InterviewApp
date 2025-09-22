using InterviewApp.Queries;
using InterviewApp.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewApp.Commands
{
    public class GetTimeGreetingHandler : IRequestHandler<GetTimeGreetingQuery, string>
    {
        private readonly ITimeGreetingService _timeGreetingService;
        private readonly ILogger<GetTimeGreetingHandler> _logger;

        public GetTimeGreetingHandler(ITimeGreetingService timeGreetingService, ILogger<GetTimeGreetingHandler> logger)
        {
            _timeGreetingService = timeGreetingService;
            _logger = logger;
        }

        public async Task<string> Handle(GetTimeGreetingQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Generating time-based greeting...");
            return await _timeGreetingService.GetTimeBasedGreetingAsync();
        }
    }
}
