using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface IEmailService
    {
        Task SendMessageAsync(string email, string url);
    }
}