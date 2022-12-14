using MoneyMe.Api.Models;
using MoneyMe.Api.Requests;
using MoneyMe.Application.Contracts;
using System;

namespace MoneyMe.Api.Validations
{
    public static class Validation
    {
        public static bool IsValid(this QuoteRequest quoteRequest, Settings moneyMeSettings)
        {
            if (string.IsNullOrWhiteSpace(quoteRequest.FirstName) ||
                    string.IsNullOrWhiteSpace(quoteRequest.LastName) ||
                    string.IsNullOrWhiteSpace(quoteRequest.Mobile) ||
                    string.IsNullOrWhiteSpace(quoteRequest.Email) ||
                    !quoteRequest.DateOfBirth.HasValue ||
                    quoteRequest.LoanAmount < moneyMeSettings.MinimumLoanAmount ||
                    quoteRequest.LoanAmount > moneyMeSettings.MaximumLoanAmount ||
                    quoteRequest.Term < 1 ||
                    !quoteRequest.IsAgeAccepted())
            {
                return false;
            }

            return true;
        }

        public static bool IsValid(this FeeRequest feeRequest)
        {
            if (string.IsNullOrWhiteSpace(feeRequest.Name) ||
                !feeRequest.Amount.HasValue ||
                !feeRequest.IsPercentage.HasValue ||
                feeRequest.Amount < 1)
            {
                return false;
            }

            return true;
        }

        public static bool IsValid(this ProductRequest productRequest)
        {
            if (string.IsNullOrWhiteSpace(productRequest.Name) ||
                !productRequest.InterestRate.HasValue ||
                !productRequest.MaximumDuration.HasValue ||
                !productRequest.MinimumDuration.HasValue ||
                Guid.Empty == productRequest.RuleId)
            {
                return false;
            }

            return true;
        }

        public static bool IsValid(this RuleRequest ruleRequest)
        {
            if (string.IsNullOrWhiteSpace(ruleRequest.Name))
            {
                return false;
            }

            return true;
        }

        public static bool CanUpdate(this FeeRequest feeRequest)
        {
            if (feeRequest.Name != null && feeRequest.Name == string.Empty)
                return false;

            if (feeRequest.Amount.HasValue && feeRequest.Amount < 1)
                return false;

            return true;
        }

        private static bool IsAgeAccepted(this QuoteRequest quoteRequest)
        {
            var currentDate = DateTime.UtcNow;
            var age = currentDate.Year - quoteRequest.DateOfBirth.Value.Year;

            if (currentDate.Month < quoteRequest.DateOfBirth.Value.Month ||
                (currentDate.Month == quoteRequest.DateOfBirth.Value.Month &&
                currentDate.Day < quoteRequest.DateOfBirth.Value.Day))
            {
                age--;
            }

            if (age < 18 || age > 74)
            {
                return false;
            }

            return true;
        }
    }
}