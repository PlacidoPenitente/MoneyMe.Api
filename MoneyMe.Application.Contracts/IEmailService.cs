using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface IEmailService
    {
        Task SendRedirectUrlAsync(string email, string url);
    }
}