using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp.Services
{
    public class TimeGreetingService : ITimeGreetingService
    {
        public string GetTimeBasedGreeting()
        {
            var now = DateTime.Now.TimeOfDay;

            if (now >= TimeSpan.FromHours(0) && now < TimeSpan.FromHours(12))
                return "Good morning";
            if (now >= TimeSpan.FromHours(12) && now < TimeSpan.FromHours(18))
                return "Good afternoon";
            if (now >= TimeSpan.FromHours(18) && now < TimeSpan.FromHours(22))
                return "Good evening";

            
            return string.Empty;
        }
    }
}
