using MoneyMe.Application.Contracts;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public Task SendMessageAsync(string email, string url)
        {
            System.Console.WriteLine(url);
            return Task.CompletedTask;
        }
    }
}