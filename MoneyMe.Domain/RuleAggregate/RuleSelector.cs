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

        private RuleSelector()
        {
            _interestFreeRule = new InterestFreeRule();
            _firstTwoMonthsInterestFreeRule = new FirstTwoMonthsInterestFreeRule();
            _lastPaymentInterestFree = new LastPaymentInterestFreeRule();
            _noInterestFreeRule = new NoInterestFreeRule();
        }

        public static RuleSelector Instance { get; } = new RuleSelector();

        public IRuleImplementation Select(string ruleName)
        {
            switch (ruleName)
            {
                case nameof(InterestFreeRule):
                    return _interestFreeRule;

                case nameof(FirstTwoMonthsInterestFreeRule):
                    return _firstTwoMonthsInterestFreeRule;

                case nameof(LastPaymentInterestFreeRule):
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