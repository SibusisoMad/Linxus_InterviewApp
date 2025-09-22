using System.Threading.Tasks;

namespace InterviewApp.Services
{
    public interface ITimeGreetingService
    {
        Task<string> GetTimeBasedGreetingAsync();
    }
}
