namespace MoneyMe.Domain.Shared
{
    public class Term
    {
        public Term(int period, decimal payment)
        {
            Period = period;
            Payment = payment;
        }

        public int Period { get; }
        public decimal Payment { get; }
    }
}