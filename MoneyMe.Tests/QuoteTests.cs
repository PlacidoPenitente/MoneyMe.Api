using MoneyMe.Application;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.QuoteAggregate;
using MoneyMe.Domain.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MoneyMe.Tests
{
    public class QuoteTests
    {
        private readonly Mock<QuoteService> _sut;

        public Mock<IQuoteFactory> QuoteFactory { get; }
        public Mock<IQuoteRepository> QuoteRepository { get; }
        public Mock<IProductRepository> ProductRepository { get; }
        public Mock<IUnitOfWork> UnitOfWork { get; }
        public Quote Quote { get; }
        public Product Product { get; }

        public QuoteTests()
        {
            QuoteFactory = new Mock<IQuoteFactory>();
            QuoteRepository = new Mock<IQuoteRepository>();
            ProductRepository = new Mock<IProductRepository>();
            UnitOfWork = new Mock<IUnitOfWork>();
            Quote = CreateQuote();
            Product = CreateProduct();

            ProductRepository.Setup(x => x.FindByNumberOfTerms(24))
                .ReturnsAsync(CreateProduct());

            QuoteFactory.Setup(x => x.Create(It.IsAny<Guid>(), 5300)).Returns(Quote);

            QuoteRepository.Setup(x => x.AddAsync(Quote));

            UnitOfWork.Setup(x => x.ExecuteAsync(It.IsAny<Func<Task>>())).Returns(() => QuoteRepository.Object.AddAsync(Quote));

            _sut = new Mock<QuoteService>(QuoteRepository.Object, QuoteFactory.Object, ProductRepository.Object, UnitOfWork.Object);
        }

        private Quote CreateQuote()
        {
            return new Quote(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, Guid.NewGuid(), 5300);
        }

        private Product CreateProduct()
        {
            return new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "", 0.0949m, 24);
        }

        [Fact]
        public async Task Should_CalculateAsync()
        {
            var partialQuote = new PartialQuoteDto() { Terms = 24, AmountRequired = 5300, CustomerId = Guid.NewGuid() };

            var monthlyPayment = 243.32m;

            var context = _sut.Object;
            var quote = await context.CalculateAsync(partialQuote);

            Assert.Equal(24, quote.Terms);
            Assert.Equal(monthlyPayment, decimal.Round(quote.MonthlyPayment, 2));

            QuoteFactory.Verify(x => x.Create(It.IsAny<Guid>(), It.IsAny<decimal>()), Times.Once);
            ProductRepository.Verify(x => x.FindByNumberOfTerms(partialQuote.Terms), Times.Once);
            QuoteRepository.Verify(x => x.AddAsync(It.IsAny<Quote>()), Times.Once);
            UnitOfWork.Verify(x => x.ExecuteAsync(It.IsAny<Func<Task>>()), Times.Once);
        }
    }
}