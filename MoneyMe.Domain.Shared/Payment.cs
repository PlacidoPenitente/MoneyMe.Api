namespace MoneyMe.Domain.Shared
{
    public class Payment
    {
        public Payment(int period, decimal interest, decimal principal)
        {
            Period = period;
            Interest = interest;
            Principal = principal;
        }

        public int Period { get; private set; }
        public decimal Interest { get; private set; }
        public decimal Principal { get; private set; }
    }
}