namespace MoneyMe.Application.Contracts
{
    public class Settings
    {
        public decimal MinimumLoanAmount { get; set; }
        public decimal MaximumLoanAmount { get; set; }
        public string Secret { get; set; }
    }
}