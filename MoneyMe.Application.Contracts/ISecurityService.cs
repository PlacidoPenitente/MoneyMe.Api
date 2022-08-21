namespace MoneyMe.Application.Contracts
{
    public interface ISecurityService
    {
        public string Encrypt(string text);

        public string Decrypt(string text);
    }
}