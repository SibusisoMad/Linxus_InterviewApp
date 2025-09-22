using System.Threading.Tasks;

namespace InterviewApp.Services
{
    public interface IGreetingService
    {
        Task<string> GetGreetingMessageAsync();
    }
}
