using MoneyMe.Application;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.QuoteAggregate;
using MoneyMe.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MoneyMe.Tests
{
    public class QuoteTests
    {
        public QuoteTests()
        {
            Products = new List<Product>
            {
                CreateProduct("Product A", 0.0949m, 3, 1, ""),
                CreateProduct("Product B", 0.0949m, 6, 1, ""),
                CreateProduct("Product C", 0.0949m, 12, 1, ""),
                CreateProduct("Product D", 0.0949m, 24, 1, ""),
            };

            QuoteFactory = new Mock<IQuoteFactory>();
            QuoteRepository = new Mock<IQuoteRepository>();
            ProductRepository = new Mock<IProductRepository>();
            UnitOfWork = new Mock<IUnitOfWork>();

            Sut = new Mock<QuoteService>(
                QuoteRepository.Object,
                QuoteFactory.Object,
                ProductRepository.Object,
                UnitOfWork.Object);

            Setup();
        }

        public Mock<QuoteService> Sut { get; }
        public Mock<IQuoteFactory> QuoteFactory { get; }
        public Mock<IRuleFactory> RuleFactory { get; }
        public Mock<IQuoteRepository> QuoteRepository { get; }
        public Mock<IProductRepository> ProductRepository { get; }
        public Mock<IUnitOfWork> UnitOfWork { get; }
        public IReadOnlyCollection<Product> Products { get; }

        [Theory]
        [InlineData(243.32, 5300, 24)]
        [InlineData(472.87, 10300, 24)]
        [InlineData(1764.49, 10300, 6)]
        [InlineData(2133.30, 6300, 3)]
        public async Task Should_CalculateAsync(decimal monthlyPayment, decimal loanAmount, int terms)
        {
            var partialQuote = new PartialQuoteDto() { Term = terms, AmountRequired = loanAmount, CustomerId = Guid.NewGuid() };
            var context = Sut.Object;
            
            var quote = await context.CalculateAsync(partialQuote);

            Assert.Equal(terms, quote.Terms);
            Assert.Equal(monthlyPayment, decimal.Round(quote.MonthlyPayment, 2));

            //QuoteFactory.Verify(x => x.Create(It.IsAny<Guid>(), It.IsAny<decimal>()), Times.Once);
            //ProductRepository.Verify(x => x.FindByNumberOfTermsAsync(partialQuote.Term), Times.Once);
            QuoteRepository.Verify(x => x.AddAsync(It.IsAny<Quote>()), Times.Once);
            UnitOfWork.Verify(x => x.ExecuteAsync(It.IsAny<Func<Task>>()), Times.Once);
        }

        private void Setup()
        {
            //ProductRepository.Setup(x => x.FindByNumberOfTermsAsync(It.IsAny<int>()))
            //                .ReturnsAsync((int terms) =>
            //                {
            //                    return Products.FirstOrDefault(x => x.MaximumDuration == terms);
            //                });

            //QuoteFactory.Setup(x => x.Create(It.IsAny<Guid>(), It.IsAny<decimal>()))
            //    .Returns((Guid id, decimal loanAmount) =>
            //    {
            //        return CreateQuote(loanAmount, 1);
            //    });

            Quote quoteToBeAdded = null;

            QuoteRepository.Setup(x => x.AddAsync(It.IsAny<Quote>())).Returns((Quote quote) =>
            {
                quoteToBeAdded = quote;
                return Task.CompletedTask;
            });

            UnitOfWork.Setup(x => x.ExecuteAsync(It.IsAny<Func<Task>>())).Returns(() => QuoteRepository.Object.AddAsync(quoteToBeAdded));
        }

        private Quote CreateQuote(decimal loanAmount, int term)
        {
            return new Quote(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, Guid.NewGuid(), loanAmount, term);
        }

        private Product CreateProduct(string productName, decimal interetestRate, int maxDuration, int minimumDuration, string ruleName)
        {
            var rule = RuleFactory.Object.CreateRule(ruleName);

            return new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, productName, interetestRate, maxDuration, minimumDuration, "");
        }
    }
}