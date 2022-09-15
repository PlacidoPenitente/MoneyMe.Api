namespace MoneyMe.Api.Requests
{
    public class FeeRequest
    {
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsPercentage { get; set; }
    }
}