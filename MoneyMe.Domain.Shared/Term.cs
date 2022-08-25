namespace MoneyMe.Domain.Shared
{
    public class Term
    {
        public Term(int period, decimal interest, decimal principal)
        {
            Period = period;
            Interest = interest;
            Principal = principal;
        }

        public int Period { get; }
        public decimal Interest { get; }
        public decimal Principal { get; }
    }
}