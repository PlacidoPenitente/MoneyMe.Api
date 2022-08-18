namespace MoneyMe.Domain.ProductAggregate
{
    public class Term
    {
        public Term(int period, decimal interest)
        {
            Period = period;
            MonthlyAmortization = interest;
        }

        public int Period { get; }
        public decimal MonthlyAmortization { get; }
    }
}