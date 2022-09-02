using MoneyMe.Api.Requests;
using System;

namespace MoneyMe.Api.Validations
{
    public static class Validation
    {
        public static bool IsValid(this QuoteRequest quoteRequest, MoneyMeSettings moneyMeSettings)
        {
            if (string.IsNullOrWhiteSpace(quoteRequest.FirstName) ||
                    string.IsNullOrWhiteSpace(quoteRequest.LastName) ||
                    string.IsNullOrWhiteSpace(quoteRequest.Mobile) ||
                    string.IsNullOrWhiteSpace(quoteRequest.Email) ||
                    !quoteRequest.DateOfBirth.HasValue ||
                    quoteRequest.AmountRequired < moneyMeSettings.MinimumLoanAmount ||
                    quoteRequest.AmountRequired > moneyMeSettings.MaximumLoanAmount ||
                    quoteRequest.Term < 1 ||
                    !quoteRequest.IsAgeAccepted())
            {
                return false;
            }

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