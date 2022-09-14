using MoneyMe.Domain.RuleAggregate.RuleImplementations;
using MoneyMe.Domain.Rules;

namespace MoneyMe.Domain.RuleAggregate
{
    public class RuleSelector
    {
        private readonly IRuleImplementation _interestFreeRule;
        private readonly IRuleImplementation _firstTwoMonthsInterestFreeRule;
        private readonly IRuleImplementation _lastPaymentInterestFree;
        private readonly IRuleImplementation _noInterestFreeRule;

        public RuleSelector()
        {
            _interestFreeRule = new InterestFreeRule();
            _firstTwoMonthsInterestFreeRule = new FirstTwoMonthsInterestFreeRule();
            _lastPaymentInterestFree = new LastPaymentInterestFree();
            _noInterestFreeRule = new NoInterestFreeRule();
        }

        public IRuleImplementation Select(string ruleName)
        {
            switch (ruleName)
            {
                case nameof(InterestFreeRule):
                    return _interestFreeRule;

                case nameof(FirstTwoMonthsInterestFreeRule):
                    return _firstTwoMonthsInterestFreeRule;

                case nameof(LastPaymentInterestFree):
                    return _lastPaymentInterestFree;

                case nameof(NoInterestFreeRule):
                    return _noInterestFreeRule;

                default:
                    break;
            }

            return null;
        }
    }
}