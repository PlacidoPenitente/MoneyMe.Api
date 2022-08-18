using MoneyMe.Domain.ProductAggregate;
using System.Collections.Generic;

namespace MoneyMe.Domain.Factories
{
    public class TermFactory
    {
        public IReadOnlyCollection<Term> CreateTerms(int count)
        {
            var terms = new List<Term>();

            for (int i = 0; i < count; i++)
            {
                var term = new Term(i, 0);
                terms.Add(term);
            }

            return terms;
        }
    }
}