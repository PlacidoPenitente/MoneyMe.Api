using MoneyMe.Application.Contracts;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public Task SendRedirectUrlAsync(string email, string url)
        {
            return Task.CompletedTask;
        }
    }
}