using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Domain.LoanAggregate
{
    public class Term
    {
        private Term()
        {

        }

        public Term(int period, decimal interest, decimal principal)
        {
            Period = period;
            Interest = interest;
            Principal = principal;
        }

        public int Period { get; private set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Interest { get; private set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Principal { get; private set; }
    }
}