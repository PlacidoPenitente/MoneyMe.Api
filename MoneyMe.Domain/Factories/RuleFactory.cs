using MoneyMe.Domain.Rules;

namespace MoneyMe.Domain.Factories
{
    public class RuleFactory : IRuleFactory
    {
        public IRule CreateRule(string ruleName)
        {
            switch (ruleName)
            {
                case nameof(InterestFreeRule):
                    return new InterestFreeRule();

                case nameof(FirstTwoMonthsInterestFreeRule):
                    return new FirstTwoMonthsInterestFreeRule();

                case nameof(LastPaymentInterestFree):
                    return new LastPaymentInterestFree();

                default:
                    break;
            }

            return new NoInterestFreeRule();
        }
    }
}